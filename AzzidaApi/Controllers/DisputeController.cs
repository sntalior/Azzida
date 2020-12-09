using AzzidaApi.Models;
using BAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace AzzidaApi.Controllers
{
    public class DisputeController : ApiController
    {
        string path = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        string ResponseOk = "{\"status\": \"1\", \"message\": \"success\", \"data\":[message]}";
        string ResponseErr = "{\"status\": \"0\", \"message\": \"error occurred\"}";
        string ResponseOther = "{\"status\": \"0\", \"message\": \"This Email is already Exist\", \"data\":[message]}";

        private string GetSerialized(object obj)
        {
            //var serializer = new JavaScriptSerializer() ;

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer() { MaxJsonLength = 86753090 };
            return javaScriptSerializer.Serialize(obj);

        }

        [HttpPost]


        // public IHttpActionResult Post(int id, string UserFullName, string Email, string UserMobile, string UserPassword, string dob, string address)
        public IHttpActionResult Post()
        {
            string retVal = string.Empty;
            MasterUpdate objmaster = new MasterUpdate();

            //   var ChckEmailExist = objmaster.ChckEmailExist(Email);
          

                HttpFileCollection UploadFile = HttpContext.Current.Request.Files;
                string Image = "";


                for (int i = 0; i < UploadFile.Count; i++)
                {

                    var files = SaveImage(UploadFile[i]);
                    Image = files;


                }

                var obj = objmaster.SaveDispute(Convert.ToInt32(HttpContext.Current.Request.Form["Id"]),
                    Convert.ToInt32(HttpContext.Current.Request.Form["UserId"]),
                    Convert.ToInt32(HttpContext.Current.Request.Form["JobId"]), 
                    HttpContext.Current.Request.Form["DisputeReason"].ToString(), 
                    HttpContext.Current.Request.Form["PostAssociate"].ToString(), 
                    //HttpContext.Current.Request.Form["ContactWay"].ToString(), 
                    HttpContext.Current.Request.Form["Description"].ToString(), Image);
                if (obj != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(obj.Attachment))
                        {
                            obj.Attachment = path + obj.Attachment;
                        }
                        retVal = GetSerialized(obj);
                        retVal = ResponseOk.Replace("[message]", retVal);
                    }
                    catch (Exception ex)
                    {
                        retVal = ResponseErr.Replace("error occurred", "failed");
                    }
                }


           
            return new RawJsonActionResult(retVal);
        }


        private string SaveImage(HttpPostedFile f)
        {
            string strImageName = "";
            string strExtension = "";

            System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\ApplicationImages\\");

            strExtension = System.IO.Path.GetExtension(f.FileName);
            strImageName = Guid.NewGuid().ToString() + strExtension;

            foreach (FileInfo file in downloadedMessageInfo.GetFiles().Where(x => x.Name == strImageName))
            {
                try
                {

                    file.Delete();
                }
                catch
                {

                }
            }

            f.SaveAs(System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\ApplicationImages\\" + strImageName);



            return strImageName;

        }
    }
}
