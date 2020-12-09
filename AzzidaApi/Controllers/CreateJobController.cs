using AzzidaApi.Models;
using BAL;
using Newtonsoft.Json;
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
    public class CreateJobController : ApiController
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
            //HttpFileCollection UploadFile = HttpContext.Current.Request.Files;
            //string Image = "";


            //for (int i = 0; i < UploadFile.Count; i++)
            //{

            //    var files = SaveImage(UploadFile[i]);
            //    Image = files;


            //}

            //List<string> Image = new List<string>();
            var image = HttpContext.Current.Request.Form["imglist"];

            if (image != null)
            {
                image = image.Replace("\\", "");
                List<string> imglist = JsonConvert.DeserializeObject<List<string>>(image);
                var obj = objmaster.CreateJob(Convert.ToInt32(HttpContext.Current.Request.Form["Id"]),
            Convert.ToInt32(HttpContext.Current.Request.Form["UserId"]),
            HttpContext.Current.Request.Form["JobTitle"],
            HttpContext.Current.Request.Form["HowLong"], HttpContext.Current.Request.Form["Amount"].ToString(),
            HttpContext.Current.Request.Form["JobCategory"], HttpContext.Current.Request.Form["Location"].ToString(),
            HttpContext.Current.Request.Form["FromDate"].ToString(), HttpContext.Current.Request.Form["JobDescription"].ToString(),
            HttpContext.Current.Request.Form["Latitude"].ToString(),
            HttpContext.Current.Request.Form["Longitude"].ToString(), imglist);
                //, Convert.ToDouble(HttpContext.Current.Request.Form["radius"])
                if (obj != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(obj.JobPicture))
                        {
                            obj.JobPicture = path + obj.JobPicture;
                        }
                        retVal = GetSerialized(obj);
                        retVal = ResponseOk.Replace("[message]", retVal);
                    }
                    catch (Exception ex)
                    {
                        retVal = ResponseErr.Replace("\\", "") + ex.Message;

                    }

                }

            }
                return new RawJsonActionResult(retVal);
            }

            //private string SaveImage(HttpPostedFile f)
            //{
            //    string strImageName = "";
            //    string strExtension = "";

            //    System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\ApplicationImages\\");

            //    strExtension = System.IO.Path.GetExtension(f.FileName);
            //    strImageName = Guid.NewGuid().ToString() + strExtension;

            //    foreach (FileInfo file in downloadedMessageInfo.GetFiles().Where(x => x.Name == strImageName))
            //    {
            //        try
            //        {

            //            file.Delete();
            //        }
            //        catch
            //        {

            //        }
            //    }

            //    f.SaveAs(System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\ApplicationImages\\" + strImageName);



            //    return strImageName;

            //}
        }
    }
