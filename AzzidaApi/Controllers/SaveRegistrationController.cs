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
    public class SaveRegistrationController : ApiController
    {
        string path = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        string ResponseOk = "{\"status\": \"1\", \"message\": \"success\", \"data\":[message]}";
        string ResponseErr = "{\"status\": \"0\", \"message\": \"error occurred\"}";
        string ResponseSuccess = "{\"status\": \"1\", \"message\": \"success\"}";
        private string GetSerialized(object obj)
        {
            //var serializer = new JavaScriptSerializer() ;

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer() { MaxJsonLength = 86753090 };
            return javaScriptSerializer.Serialize(obj);
        }

        [HttpPost]

        public IHttpActionResult Post()
        {
            string retVal = string.Empty;
            MasterUpdate objmaster = new MasterUpdate();
            HttpFileCollection UploadFile = HttpContext.Current.Request.Files;
            string Image = "";


            for (int i = 0; i < UploadFile.Count; i++)
            {

                var files = SaveImage(UploadFile[i]);
                Image = files;


            }


            var data = objmaster.IsEmailExist(HttpContext.Current.Request.Form["UserEmail"]);
            var data1 = objmaster.IsUserNameExist(HttpContext.Current.Request.Form["UserName"]);

            if (data == null || Convert.ToInt32(HttpContext.Current.Request.Form["Id"]) > 0)
            {
                if (data1 == null || Convert.ToInt32(HttpContext.Current.Request.Form["Id"]) > 0)
                {
                    DAL.UserMaster RefcodeExist = new DAL.UserMaster();
                    if (HttpContext.Current.Request.Form["ReferalCode"] != "")
                    {
                        RefcodeExist = objmaster.IsRefCodeExist(HttpContext.Current.Request.Form["ReferalCode"]);

                        if (RefcodeExist != null)
                        {
                            var obj = objmaster.Register(Convert.ToInt32(HttpContext.Current.Request.Form["Id"]), Convert.ToInt32(HttpContext.Current.Request.Form["RoleId"]), HttpContext.Current.Request.Form["FirstName"], HttpContext.Current.Request.Form["LastName"],
                                HttpContext.Current.Request.Form["UserPassword"], HttpContext.Current.Request.Form["UserEmail"], HttpContext.Current.Request.Form["Skills"], HttpContext.Current.Request.Form["DeviceId"],
                                HttpContext.Current.Request.Form["DeviceType"], HttpContext.Current.Request.Form["EmailType"], HttpContext.Current.Request.Form["UserName"], Image, HttpContext.Current.Request.Form["JobType"], HttpContext.Current.Request.Form["ReferalCode"], HttpContext.Current.Request.Form["StripeAccId"]);
                            if (obj != null)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(obj.ProfilePicture))
                                    {
                                        obj.ProfilePicture = path + obj.ProfilePicture;
                                    }
                                    retVal = GetSerialized(obj);
                                    retVal = ResponseOk.Replace("[message]", retVal);
                                }
                                catch (Exception ex)
                                {
                                    retVal = ResponseErr.Replace("error occurred", "failed");

                                }
                            }
                        }
                        else
                        {
                            retVal = ResponseErr.Replace("error occurred", "Referal Code is not valid.");
                        }
                    }
                    else
                    {
                        var obj = objmaster.Register(Convert.ToInt32(HttpContext.Current.Request.Form["Id"]), Convert.ToInt32(HttpContext.Current.Request.Form["RoleId"]), HttpContext.Current.Request.Form["FirstName"], HttpContext.Current.Request.Form["LastName"],
                               HttpContext.Current.Request.Form["UserPassword"], HttpContext.Current.Request.Form["UserEmail"], HttpContext.Current.Request.Form["Skills"], HttpContext.Current.Request.Form["DeviceId"],
                               HttpContext.Current.Request.Form["DeviceType"], HttpContext.Current.Request.Form["EmailType"], HttpContext.Current.Request.Form["UserName"], Image, HttpContext.Current.Request.Form["JobType"], HttpContext.Current.Request.Form["ReferalCode"], HttpContext.Current.Request.Form["StripeAccId"]);
                        if (obj != null)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(obj.ProfilePicture))
                                {
                                    obj.ProfilePicture = path + obj.ProfilePicture;
                                }
                                retVal = GetSerialized(obj);
                                retVal = ResponseOk.Replace("[message]", retVal);
                            }
                            catch (Exception ex)
                            {
                                retVal = ResponseErr.Replace("error occurred", "failed");

                            }
                        }
                    }
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "User name is already exist.");
                }

            }
            else
            {
                retVal = ResponseErr.Replace("error occurred", "Email is already exist.");
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
