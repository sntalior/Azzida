using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class JobList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillJobList();
            }
        }

        public void fillJobList()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var data = objmaster.GetJobList();
            if (data != null)
            {
                if (txtt.Text != "")
                {
                    data = data.Where(x => (x.JobTitle.ToLower().Contains(txtt.Text.ToLower()) || (x.JobCategory.ToLower().Contains(txtt.Text.ToLower())))).ToList();
                  
                }

                grdJob.DataSource = data;
                grdJob.DataBind();
            }
        }

        protected void grdJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdJob.PageIndex = e.NewPageIndex;
            fillJobList();
        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            fillJobList();
        }
    }
}