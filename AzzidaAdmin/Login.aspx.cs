using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class Login : System.Web.UI.Page
    {
        private double unixTimeStamp;

        protected void Page_Load(object sender, EventArgs e)
        {


            //string startdatetime = "1605586658161";

            //var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(startdatetime));
            //var date2 = System.DateTime.Now;
            //var hours = (date2 - date).TotalHours;
          //string str= " application for "+'"' + "JobTitle "+'"'+ "";
          // ltrMsg.Text = str;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            MasterUpdate masterobj = new MasterUpdate();
            var obj = masterobj.LoginAdmin(txtUserName.Text, txtPassword.Text);

            if (obj != null)
            {
                Session.Timeout = 20;
                userInfo info = new userInfo();
                info.ID = obj.Id;
                info.FirstName = obj.FirstName;
                LoginSession.userInfo = info;
                Response.Redirect("UserManager.aspx");
            }

            else
            {
                Response.Write("<script>alert('UserName and Password is not Matched')</script>");
            }
        }
    }
}