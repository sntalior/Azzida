using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class PaymentHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPaymentHistoryList();
            }
        }

        public void FillPaymentHistoryList()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var data = objmaster.GetPaymentHistoryList();

            if (data != null)
            {
                if (txtSearch.Text != "")
                {
                    //foreach(var a in data)
                    //{
                    if (drpStatus.SelectedValue != "0")
                    {
                        data = data.Where(x => x.Status.Contains(drpStatus.SelectedValue) && x.JobTitle.ToUpper().Contains(txtSearch.Text.ToUpper()) || x.ListerName.ToUpper().Contains(txtSearch.Text.ToUpper())
                     || Convert.ToString(x.TotalAmount).Contains(txtSearch.Text)).ToList();
                    }
                    else
                    {
                        data = data.Where(x => x.JobTitle.ToUpper().Contains(txtSearch.Text.ToUpper()) || x.ListerName.ToUpper().Contains(txtSearch.Text.ToUpper())
                     || Convert.ToString(x.TotalAmount).Contains(txtSearch.Text)).ToList();
                    }


                    //}
                }
                else
                {
                    if (drpStatus.SelectedValue != "0")
                    {
                        data = data.Where(x => x.Status.Contains(drpStatus.SelectedValue)).ToList();
                    }

                }
                if (data.Count > 0)
                {
                    gvPaymentHistoryList.DataSource = data;
                    gvPaymentHistoryList.DataBind();
                    gvPaymentHistoryList.Visible = true;
                }
                else
                {
                    lbldata.Text = "No Record Found";
                    gvPaymentHistoryList.Visible = false;
                }
            }
        }

        protected void gvPaymentHistoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPaymentHistoryList.PageIndex = e.NewPageIndex;
            FillPaymentHistoryList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillPaymentHistoryList();
        }

        //protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillPaymentHistoryList();
        //}
    }
}