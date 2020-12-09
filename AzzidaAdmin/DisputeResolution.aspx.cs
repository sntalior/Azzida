using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class DisputeResolution : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDisputeResolution();
            }
        }

        public void FillDisputeResolution()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var data = objmaster.GetDisputeResolution();
            if (data != null)
            {

                GrdDisputeResolution.DataSource = data;
                GrdDisputeResolution.DataBind();
            }

        }



        protected void GrdDisputeResolution_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdDisputeResolution.PageIndex = e.NewPageIndex;
            FillDisputeResolution();
        }

        protected void GrdDisputeResolution_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnid = (HiddenField)e.Row.FindControl("hdnid");
                LinkButton Attach = (LinkButton)e.Row.FindControl("Attach");
                MasterUpdate objmaster = new MasterUpdate();
                var data = objmaster.GetDisputeById(Convert.ToInt32(hdnid.Value));
                if (!string.IsNullOrEmpty(data.Attachment))
                {
                    Attach.Visible = true;

                }
                else
                {
                    Attach.Visible = false;
                }
            }
        }

        protected void GrdDisputeResolution_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Attach")
            {
                MasterUpdate objmaster = new MasterUpdate();
                var data = objmaster.GetDisputeById(Convert.ToInt32(e.CommandArgument));
                if (!string.IsNullOrEmpty(data.Attachment))
                {


                    GridViewRow gr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    LinkButton Attach = (LinkButton)gr.FindControl("Attach");
                    Attach.Attributes.Add("target", "_blank");
                    Response.Redirect("http://13.72.77.167:8086/ApplicationImages/" + data.Attachment);
                }
            }
        }
    }
}
