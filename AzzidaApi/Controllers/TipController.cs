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
    public class TipController : ApiController
    {

        string path = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        string ResponseOk = "{\"status\": \"1\", \"message\": \"success\", \"data\":[message]}";
        string ResponseErr = "{\"status\": \"0\", \"message\": \"error occurred\"}";
        string ResponseErrwithData = "{\"status\": \"0\", \"message\": \"error occurred\", \"data\":[message]}";
        string ResponseSuccess = "{\"status\": \"1\", \"message\": \"success\"}";
        private string GetSerialized(object obj)
        {
            //var serializer = new JavaScriptSerializer() ;

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer() { MaxJsonLength = 86753090 };
            return javaScriptSerializer.Serialize(obj);
        }

        [HttpPost]

        public IHttpActionResult PostTip(int Id,int UserId, int JobId, string TippingAmount, string TotalAmount, int SeekerId, decimal SeekerRate, int paymentId)
        {
            string retVal = string.Empty;
            MasterUpdate objmaster = new MasterUpdate();

            //var data = objmaster.IsEmailExist(UserEmail);
            //if (data == null)
            //{

            var obj = objmaster.Tipp(Id, UserId, JobId, TippingAmount, TotalAmount, SeekerId,SeekerRate, paymentId);
            if (obj != null)
            {
                //if (obj.Status == "Success")
                //{
                    try
                    {
                        retVal = GetSerialized(obj);
                        retVal = ResponseOk.Replace("[message]", retVal);
                    }
                    catch (Exception ex)
                    {
                        retVal = ResponseErr.Replace("error occurred", "failed");

                    }
                //}
                //else
                //{
                //    retVal = GetSerialized(obj);
                //    retVal = ResponseErrwithData.Replace("error occurred", obj.Status);
                //}
            }

            return new RawJsonActionResult(retVal);
        }

    }
}


            