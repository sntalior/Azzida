using BAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class UserManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSession.userInfo == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                //profilePicture.ImageUrl = "dist/img/BackgroundImage.jpg";
                //drprole.Items.Insert(0, new ListItem("--Select Role--", "0"));
                BindRole();
                fillUserList();

            }
            ltrError.Text = "";
            dvError.Visible = false;

        }

        public void BindRole()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var data = objmaster.GetRoles();
            drprole.DataSource = data;
            drprole.DataTextField = "Role";
            drprole.DataValueField = "Id";
            drprole.DataBind();
            drprole.Items.Insert(0, new ListItem("--Select Role--", "0"));
        }

        public void fillUserList()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var data = objmaster.GetUserList();
            gvUser.DataSource = data;
            gvUser.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string SaveImagePath = System.Configuration.ConfigurationManager.AppSettings["SaveImagePath"];

            MasterUpdate objmaster = new MasterUpdate();

            string ImageFile = "";
            if (ProfilePicture1.HasFile)
            {
                ImageFile = ProfilePicture1.FileName;
                string filepath = SaveImagePath;
                string ext = Path.GetExtension(ProfilePicture1.FileName);
                ImageFile = ImageFile.Replace(ext, "");
                ImageFile = ImageFile.Replace(".", "");
                ImageFile = Guid.NewGuid().ToString() + ext;
                string savepath = filepath + "/" + ImageFile;
                ProfilePicture1.SaveAs(savepath);

                //  profilePicture.ImageUrl = "dist/img/BackgroundImage.jpg";
            }
            objmaster.Register(Convert.ToInt32(hdnUserValue.Value), Convert.ToInt32(drprole.SelectedValue), txtfirstName.Text, txtLastName.Text,
                txtPassword.Text, txtEmail.Text, txtskills.Text, "", "", drpEmailType.SelectedValue, txtUserName.Text, ImageFile, "","","");
            drprole.ClearSelection();
            hdnUserValue.Value = "0";
            //drprole.SelectedValue = "0";
            txtfirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtskills.Text = "";
            txtUserName.Text = "";
            //txtdeviceId.Text = "";
            //txtdevicetype.Text = "";

            ltrError.Text = "Successfully Save";
            dvError.Visible = true;
            fillUserList();
            // profilePicture.ImageUrl = "dist/img/BackgroundImage.jpg";
        }


        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            fillUserList();

        }

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "ViewChat")
            {
                MasterUpdate objmaster = new MasterUpdate();
                //var data = objmaster.GetUserChatList(Convert.ToInt32(e.CommandArgument));
                //if (data.Count > 0)
                //{
                    Response.Redirect("UserChat.aspx?Id=" + e.CommandArgument);
                //}
               
            }
            if (e.CommandName == "UpdateItem")
            {
                MasterUpdate objmaster = new MasterUpdate();
                var data = objmaster.GetUserById(Convert.ToInt32(e.CommandArgument));
                try
                {
                    drprole.SelectedValue = data.RoleId.ToString();
                }
                catch { }
                hdnUserValue.Value = data.Id.ToString();
                txtfirstName.Text = data.FirstName;
                txtLastName.Text = data.LastName;
                txtEmail.Text = data.UserEmail;
                txtPassword.Text = data.UserPassword;
                try
                {
                    drpEmailType.SelectedValue = data.EmailType.ToString();
                }
                catch { }

                txtConfirmPassword.Text = data.UserPassword;
                txtskills.Text = data.Skills;
                txtUserName.Text = data.UserName;

                //txtdeviceId.Text = data.DeviceId;
                //txtdevicetype.Text = data.DeviceType;
            }
            if (e.CommandName == "Activate")
            {
                MasterUpdate objmaster = new MasterUpdate();
                var data = objmaster.UserActiveDeactive(Convert.ToInt32(e.CommandArgument));
                //    if (data.IsActive.ToString() == "ture")
                //    {
                //        btnactivate.Text = "Deactive";
                //    }
                //    else
                //    {
                //        btnactivate.Text = "Active";
                //    }
                fillUserList();
            }
            if (e.CommandName == "DeleteItem")
            {
                MasterUpdate objmaster = new MasterUpdate();
                objmaster.DeleteUserById(Convert.ToInt32(e.CommandArgument));
                fillUserList();
            }
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterUpdate objmaster = new MasterUpdate();
                LinkButton lnkActivate = (e.Row.FindControl("lnkActivate") as LinkButton);
                LinkButton lnkView = (e.Row.FindControl("lnkView") as LinkButton);
                HiddenField hdnid = (e.Row.FindControl("hdnid") as HiddenField);
                var data = objmaster.GetUserById(Convert.ToInt32(hdnid.Value));

                if (data.IsActive == true)
                {
                    lnkActivate.Text = "Suspend";
                }
                else
                {
                    lnkActivate.Text = "Suspended";
                }

                var data1 = objmaster.GetUserChatList(Convert.ToInt32(hdnid.Value));
                if (data1.Count > 0)
                {
                    lnkView.Visible = true;
                }
                else
                {
                    lnkView.Visible = false;
                }
            }
        }

        //protected void btnactivate_Click(object sender, EventArgs e)
        //{
        //    MasterUpdate objmaster = new MasterUpdate();

        //    string SaveImagePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];
        //    string ImageFile = "";
        //    if (ProfilePicture1.HasFile)
        //    {
        //        ImageFile = ProfilePicture1.FileName;
        //        string filepath = SaveImagePath;
        //        string ext = Path.GetExtension(ProfilePicture1.FileName);
        //        ImageFile = ImageFile.Replace(ext, "");
        //        ImageFile = ImageFile.Replace(".", "");
        //        ImageFile = Guid.NewGuid().ToString() + ext;
        //        string savepath = filepath + "/" + ImageFile;
        //        ProfilePicture1.SaveAs(savepath);


        //    }
        //    var data1 = objmaster.Register(Convert.ToInt32(hdnUserValue.Value), Convert.ToInt32(drprole.SelectedValue), txtfirstName.Text, txtLastName.Text,
        //   txtPassword.Text, txtEmail.Text, txtskills.Text, "", "", drpEmailType.SelectedValue, txtUserName.Text, ImageFile);
        //    hdnUserValue.Value = data1.Id.ToString();


        //    var data = objmaster.UserActiveDeactive(Convert.ToInt32(hdnUserValue.Value));
        //    if (data.IsActive.ToString() == "ture")
        //    {
        //        btnactivate.Text = "Deactive";
        //    }
        //    else
        //    {
        //        btnactivate.Text = "Active";
        //    }
        //    hdnUserValue.Value = "0";
        //    txtfirstName.Text = "";
        //    txtLastName.Text = "";
        //    txtPassword.Text = "";
        //    drpEmailType.ClearSelection();
        //    txtEmail.Text = "";
        //    txtskills.Text = "";
        //    //txtdeviceId.Text = "";
        //    //txtdevicetype.Text = "";
        //    ltrError.Text = "Successfully Activate";
        //    fillUserList();
        //}
    }
}