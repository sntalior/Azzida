using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using AzzidaApi.Models;
using BAL;

namespace AzzidaApi.Controllers
{
    public class CreatePaymentController : ApiController
    {
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

        public IHttpActionResult Post(int JobId, int UserId, int ToUserId, decimal refbalance, string CustomerId, decimal TotalAmount, string PaymentToken, string PaymentType)
        {
            string retVal = string.Empty;
            MasterUpdate objmaster = new MasterUpdate();


            var obj = objmaster.CreatePayment(JobId, UserId, ToUserId, refbalance, CustomerId, TotalAmount, PaymentToken, PaymentType);
            if (obj != null)
            {
                try
                {
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

    }
}
