using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class Userchat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    fillchatList();
                }
            }

        }

        public void fillchatList()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var userdetails = objmaster.GetUserDetails(Convert.ToInt32(Request.QueryString["Id"]));
            var data = objmaster.GetUserChatList(Convert.ToInt32(Request.QueryString["Id"]));
            if (data != null)
            {
                gvUserChats.DataSource = data;
                gvUserChats.DataBind();
                username.InnerText = userdetails.UserName;
            }

        }

        protected void gvUserChats_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewChat")
            {
                userconversion.Visible = true;
                MasterUpdate objmaster = new MasterUpdate();
                var receiverdata = objmaster.GetUserDetails(Convert.ToInt32(e.CommandArgument));
                GridViewRow grdr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdnJobId = (HiddenField)grdr.FindControl("hdnIsJobId");
                var data = objmaster.GetUserChats(Convert.ToInt32(Request.QueryString["Id"]), Convert.ToInt32(e.CommandArgument), Convert.ToInt32(hdnJobId.Value));
                if (data != null)
                {
                    ReceiverName.InnerText = receiverdata.UserName;
                    rptrchatconversion.DataSource = data;
                    rptrchatconversion.DataBind();
                }
                // fillconversion();
            }
        }

        protected void rptrchatconversion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.AlternatingItem) || e.Item.ItemType.Equals(ListItemType.Item))
            {
                HiddenField hdnsenderId = (HiddenField)e.Item.FindControl("hdnsenderId");
                Image imgid = (Image)e.Item.FindControl("imgid");
                Image imgid1 = (Image)e.Item.FindControl("imgid1");
                //Control selfUser = e.Item.FindControl("selfUser");
                //Control otheruser = e.Item.FindControl("otheruser");
                Label lblsendermessage = (Label)e.Item.FindControl("lblsendermessage");
                Label txtReceivermsg = (Label)e.Item.FindControl("txtReceivermsg");
                Label lblUsrName = (Label)e.Item.FindControl("lblUsrName");
                Label lblsenderName = (Label)e.Item.FindControl("lblsenderName");
                if (hdnsenderId.Value == Request.QueryString["Id"])
                {
                   // selfUser.Visible = true;
                   // otheruser.Visible = false;

                    lblsendermessage.Visible = true;
                    lblUsrName.Visible = true;
                    imgid.Visible = true;
                    imgid1.Visible = false;
                        lblsenderName.Visible = false;
                    txtReceivermsg.Visible = false;
                }
                else
                {
                   // selfUser.Visible = false;
                   // otheruser.Visible = true;
                    lblsendermessage.Visible = false;
                    lblUsrName.Visible = false;
                    lblsenderName.Visible = true;
                    txtReceivermsg.Visible = true;
                    imgid.Visible = false;
                    imgid1.Visible = true;
                }
            }
        }
    }
}