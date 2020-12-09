﻿using AzzidaApi.Models;
using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace AzzidaApi.Controllers
{
    public class JobApplicationController : ApiController
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

        public IHttpActionResult Post(int Id, int SeekerId, int ListerId, int JobId, string IsApply)
        {
            string retVal = string.Empty;
            MasterUpdate objmaster = new MasterUpdate();

            //var data = objmaster.IsEmailExist(UserEmail);
            //if (data == null)
            //{

            var obj = objmaster.SaveApplicationStatus(Id, SeekerId, ListerId, JobId, IsApply);
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

