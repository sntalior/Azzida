using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzzidaAdmin
{
    public partial class JobFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filljobfeelist();
            }

        }

        public void filljobfeelist()
        {
            MasterUpdate objmaster = new MasterUpdate();
            var data = objmaster.GetJobFee();
            if (data != null)
            {
                hdnjobfee.Value = data.Id.ToString();
                txtseekerfee.Text = data.JobSeekerFee.ToString();
                txtListerfee.Text = data.JobListerFee.ToString();
                txtcancelfee.Text =data.CancelJobFee == null ? "" : data.CancelJobFee.ToString();
                txtbckground.Text = data.BackgroundCheck.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MasterUpdate objmaster = new MasterUpdate();
            if (Convert.ToInt32(hdnjobfee.Value) == 1)
            {
                objmaster.SaveJobFee(Convert.ToInt32(hdnjobfee.Value), txtseekerfee.Text, txtListerfee.Text, txtbckground.Text, txtcancelfee.Text);
                //hdnjobfee.Value = "0";
                //txtseekerfee.Text = "";
                //txtListerfee.Text = "";
                filljobfeelist();
            }
        }
    }
}