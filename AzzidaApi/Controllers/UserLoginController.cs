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
    public class UserLoginController : ApiController
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

        public IHttpActionResult Post(string UserName, string UserPassword, string deviceId, string devicetype)
        {
            string retVal = string.Empty;
            MasterUpdate objmaster = new MasterUpdate();



            var obj = objmaster.UserLogin(UserName, UserPassword, deviceId, devicetype);
            if (obj != null)
            {
                if (obj.IsVerified == true)
                {
                    if (obj.IsActive == true)
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
                            retVal = ResponseErr.Replace("\\", "") + ex.Message;

                        }
                    }
                    else
                    {
                        retVal = ResponseErr.Replace("error occurred", "This user is suspended.");
                    }
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "Please verify your email.");
                }
            }
            else
            {
                retVal = ResponseErr.Replace("error occurred", "Invalid Credentials.");
            }



            return new RawJsonActionResult(retVal);
        }


    }
}
