using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ComplaintManagementPortalProject.Models;
using DataAcessLayer;
using BusinessLayer;
using System.IO;
using System.Web.Mvc;

namespace ComplaintManagementPortalProject.WebService.Controllers
{
    public class PublicAPIController : Controller
    {
        // GET Countries LookUp

        public ActionResult GetCountriesAPI()
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    var countries = publicBL.GetCountries();
                    return Json(countries);
                }
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return null;
            }
        }
        //Tyr this and add api to path
        //public IHttpActionResult Get()
        //{
        //    return Ok(new
        //    {
        //        CaseID = string.Empty,
        //        Status = "OK",
        //        ErrorMessage = " Testing"
        //    });
        //}

        //Logging Error Inside File 
        public void LoggedError(string error)
        {
            var path = @"C:\Users\Mousa\source\repos\ComplaintManagementPortalProject\Logerror.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write("PublicAPIController : " + error);
            }
        }
    }
}