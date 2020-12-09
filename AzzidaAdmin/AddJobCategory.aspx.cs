using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class AddJobCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSession.userInfo == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                fillJobCategoryList();
            }
            ltrErr.Text = "";
            dvEr.Visible = false;
        }

        public void fillJobCategoryList()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var data=objmaster.GetJobCategory();
            if (data != null)
            {
                grdJobCategory.DataSource = data;
                grdJobCategory.DataBind();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MasterUpdate objmaster = new MasterUpdate();
            objmaster.CreateJobCategory(Convert.ToInt32(hdncatId.Value), txtCategoryName.Text);
            hdncatId.Value = "0";
            txtCategoryName.Text = "";
            ltrErr.Text = "Successfully Save";
            dvEr.Visible = true;
            fillJobCategoryList();

        }

        protected void grdJobCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdJobCategory.PageIndex = e.NewPageIndex;
            fillJobCategoryList();
        }

        protected void grdJobCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "UpdateItem")
            {
                MasterUpdate objmaster = new MasterUpdate();
                var data=objmaster.GetJobCategoryById(Convert.ToInt32(e.CommandArgument));
                txtCategoryName.Text = data.CategoryName;
                hdncatId.Value = data.Id.ToString();
            }

            if(e.CommandName== "DeleteItem")
            {
                MasterUpdate objmaster = new MasterUpdate();
                objmaster.DeleteCategoryById(Convert.ToInt32(e.CommandArgument));
                fillJobCategoryList();
            }
        }
    }
}