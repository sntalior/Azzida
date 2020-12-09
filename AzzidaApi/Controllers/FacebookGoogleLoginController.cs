using AzzidaApi.Models;
using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace AzzidaApi.Controllers
{
    public class FacebookGoogleLoginController : ApiController
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

        public IHttpActionResult Post(string Email, string UserName, string TokenId, string deviceId, string devicetype, string Provider)
        {
            string retVal = string.Empty;
            MasterUpdate objmaster = new MasterUpdate();
            //var data = IsExistFOrGId(Email, TokenId, Provider);
            //if (data == null)
            //{


            var obj = objmaster.FacebookGoogleLogin(Email, UserName, TokenId, deviceId, devicetype, Provider);
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
            //}
            //else
            //{
            //    retVal = ResponseErr.Replace("error occurred", "Email is already exist.");
            //}
            return new RawJsonActionResult(retVal);
        }
    }
}
