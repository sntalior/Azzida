using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["VerifiedId"] != null)
            {
                MasterUpdate obj = new MasterUpdate();

                var data = obj.CheckverificationnId(Request.QueryString["VerifiedId"].ToString());
                if (data != null)
                {
                    obj.ConfirmEmail(Request.QueryString["VerifiedId"].ToString());
                   // btnsave.Visible = false;
                    SuccessMessage.Visible = true;
                    //boxbody.Visible = false;
                    //boxfooter.Visible = false;
                    ltrMsg.Text = "Email verified succesfully.";
                    // Response.Write("<script>alert('Email is verified succesfully.')</script>");
                    if (Request.QueryString["ReferalCode"] != null)
                    {
                        obj.AddReferalBalance(data.Id, Request.QueryString["ReferalCode"]);
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please check email.')</script>");
                    SuccessMessage.Visible = false;
                    //boxbody.Visible = true;
                    //boxfooter.Visible = true;
                }
               
            }


        }

        //protected void btnsave_Click(object sender, EventArgs e)
        //{
        //    MasterUpdate obj = new MasterUpdate();
        //    // string email = "";
        //    //if (Request.QueryString["Email"] != null)
        //    //{
        //    //    email = Request.QueryString["Email"].ToString();
        //    //}
        //    if (Request.QueryString["VerifiedId"] != null)
        //    {
             
           
        //    var data=obj.CheckverificationnId(txtemailverify.Text, Request.QueryString["VerifiedId"].ToString());
        //    if (data != null)
        //    {
        //        obj.ConfirmEmail(txtemailverify.Text, Request.QueryString["VerifiedId"].ToString());
        //            btnsave.Visible = false;
        //            SuccessMessage.Visible = true;
        //            boxbody.Visible = false;
        //            boxfooter.Visible = false;
        //            ltrMsg.Text = "Email verified succesfully.";
        //       // Response.Write("<script>alert('Email is verified succesfully.')</script>");
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('Please check email.')</script>");
        //            SuccessMessage.Visible = false;
        //            boxbody.Visible = true;
        //            boxfooter.Visible = true;
        //        }
        //    }

        //}
    }
}