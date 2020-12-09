using System;
using AzzidaApi.Models;
using BAL;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace AzzidaApi.Controllers
{
    public class SeekerJobCancelledListController : ApiController
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

        [HttpGet]

        public IHttpActionResult Get(int UserId)
        {
            string retVal = string.Empty;
            MasterUpdate objmaster = new MasterUpdate();



            var obj = objmaster.SeekerJobCancelledList(UserId);

            if (obj != null)
            {

                try
                {

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
                retVal = ResponseErr.Replace("error occurred", "Data not found.");
            }



            return new RawJsonActionResult(retVal);
        }
    }
}
