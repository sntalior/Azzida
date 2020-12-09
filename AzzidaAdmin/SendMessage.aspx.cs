using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class SendMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                fillUserList();

            }
        }

        public void fillUserList()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var data = objmaster.GetUserList();
            gvUser.DataSource = data;
            gvUser.DataBind();
        }

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "sendmsg")
            {
                hdnuserId.Value = e.CommandArgument.ToString();
                ModalPopupExtender1.Show();
                sentmsgdiv.Visible = true;
            }
        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            fillUserList();
        }

        protected void btnsendmsg_Click(object sender, EventArgs e)
        {
            MasterUpdate objmatser = new MasterUpdate();
            var data = objmatser.SendAdminMessage(Convert.ToInt32(LoginSession.userInfo.ID), Convert.ToInt32(hdnuserId.Value), txtmessage.Text);
            if (data != null)
            {
                txtmessage.Text = "";
                hdnuserId.Value = "0";
                lblsuccessmsg.Text = "Message sent successfully";
                sentmsgdiv.Visible = false;
            }
            else
            {
                lblsuccessmsg.Text = "";
                sentmsgdiv.Visible = false;
            }

        }
    }
}