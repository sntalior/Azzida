using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSession.userInfo == null)
            {
                Response.Redirect("Login.aspx");
            }

            else
            {
                //if (LoginSession.isadmin)
                //{
                //    ulmanageadmin.Visible = false;
                //}

                //else
                //{
                //int roleid = LoginSession.userInfo.User;
                ltUser.Text = LoginSession.userInfo.FirstName;




                // }

                Session.Timeout = 20;
            }
        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            LoginSession.userInfo = null;
            Response.Redirect("Login.aspx");
        }
    }
}