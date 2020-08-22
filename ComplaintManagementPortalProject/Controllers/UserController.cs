using ComplaintManagementPortalProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ComplaintManagementPortalProject.Controllers
{

    public class UserController : Controller
    {
        string Baseurl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/";
        [HttpGet]

        // GET: DefaultPage
        public ActionResult Index()
        {
            return View();
        }

        //Get the view to fill it with information
        [HttpGet]
        public ActionResult SignUp()
        {
            try
            {
                SystemUsers _model = new SystemUsers();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    List<Countries> CountriesListModel = null;
                    var CountriesListAPI = client.GetAsync("api/PublicAPI/GetCountriesAPI/").Result;
                    if (CountriesListAPI.IsSuccessStatusCode)
                    {
                        var CountriesListIDs = CountriesListAPI.Content.ReadAsStringAsync().Result;

                        CountriesListModel = JsonConvert.DeserializeObject<List<Countries>>(CountriesListIDs);

                        _model.Coutries = (from d in CountriesListModel
                                           select new SelectListItem
                                                    {
                                                        Value = d.CountryID.ToString(),
                                                        Text = d.Name
                                                    }).ToList();
                    }
                }

                    return View(_model);
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("User", "ServerError");
            }
        }

        //Send the information and proccess it 
        [HttpPost]
        public ActionResult SignUp(SystemUsers _Model)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Join()
        {
            try
            {
                SystemUsers _model = new SystemUsers();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    List<Countries> CountriesListModel = null;
                    var CountriesListAPI = client.GetAsync("api/PublicAPI/GetCountriesAPI/").Result;
                    if (CountriesListAPI.IsSuccessStatusCode)
                    {
                        var CountriesListIDs = CountriesListAPI.Content.ReadAsStringAsync().Result;

                        CountriesListModel = JsonConvert.DeserializeObject<List<Countries>>(CountriesListIDs);

                        _model.Coutries = (from d in CountriesListModel
                                           select new SelectListItem
                                           {
                                               Value = d.CountryID.ToString(),
                                               Text = d.Name
                                           }).ToList();
                    }
                }

                return View(_model);
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("User", "ServerError");
            }
        }

        [HttpPost]
        public ActionResult LoggedError(string error)
        {
            var path = @"C:\Users\Mousa\source\repos\ComplaintManagementPortalProject\Logerror.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write("UserController: " +error);
            }
            return null;
        }
    }

}
