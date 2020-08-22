using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        string Baseurl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/";
        [HttpGet]

        // GET: DefaultPage
        private LogginUser GetUser()
        {
            LogginUser result = null;
            if (Session["UserProfile"] != null)
            {
                result = Session["UserProfile"] as LogginUser;
            }

            return result;
        }
        public ActionResult Index()
        {
            LogginUser userLogin = GetUser();
            if (userLogin == null)
            {
                return RedirectToAction("SignIn", "Users");

            }
            else
            {
                ViewBag.name = userLogin.FullName;
                ViewBag.Usertype = userLogin.UserTypeID;
            }
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LogginUser Model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var API = client.GetAsync(string.Format("api/PublicAPI/Login?Username={0}&Password={1}", Model.UserName,Model.Password)).Result;
                    if (API.IsSuccessStatusCode)
                    {
                        var APIResults = API.Content.ReadAsStringAsync().Result;
                        LogginUser MyModel = JsonConvert.DeserializeObject<LogginUser>(APIResults);
                        if (!string.IsNullOrEmpty(MyModel.FullName))
                        {
                            Model.FullName = MyModel.FullName;
                            Model.UserTypeID = MyModel.UserTypeID;
                            Model.UserID = MyModel.UserID;
                            ViewBag.name = Model.FullName;
                            ViewBag.Usertype = Model.UserTypeID;
                            SaveSession(MyModel);
                            if (Model.UserTypeID == 1)
                            {
                                return RedirectToAction("MyComplaints", "Complaints", new
                                {
                                    UserID = Model.UserID
                                });
                            }
                            else
                            {
                                return RedirectToAction("AllComplaints", "Complaints");
                            }

                        }
                        else 
                        { 
                            ViewBag.Error = "Wrong Email Or Password";
                            return View();
                        }
                       
                        
                    }
                }
                    return View();
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("Index", "Users");
            }
        }

        private void SaveSession(LogginUser user)
        {
            Session["UserProfile"] = user;
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


                        _model.Countries = (from d in CountriesListModel
                                            select new SelectListItem
                                            {
                                                Value = d.CountryID.ToString(),
                                                Text = d.CountryName
                                            }).ToList();
                    }
                }

                return View(_model);
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("Index","Users");
            }
        }

        //Send the information and proccess it 
        [HttpPost]
        public ActionResult SignUp(SystemUsers _Model)
        {
            if (!ModelState.IsValid)
            {
                SignUpValid(_Model);
                return View();
            }
            else
            {
                //Check Username And Email if exsist

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var API = client.GetAsync(string.Format("api/PublicAPI/CheckIfEmailExsists?Email={0}", _Model.Email)).Result;
                    if (API.IsSuccessStatusCode)
                    {
                        Boolean Exsists = Convert.ToBoolean(API.Content.ReadAsStringAsync().Result);
                        if (Exsists == true)
                        {
                            ViewBag.Error = "Email Already Exists";
                            SignUpValid(_Model);
                            return View();
                        }
                    }
                        var APIForUserName = client.GetAsync(string.Format("api/PublicAPI/CheckIfUserNameExsists?UserName={0}", _Model.UserName)).Result;
                        if (APIForUserName.IsSuccessStatusCode)
                        {
                            Boolean Exsist = Convert.ToBoolean(APIForUserName.Content.ReadAsStringAsync().Result);
                            if (Exsist == true)
                            {
                                ViewBag.Error = "Username Already Exists";
                                SignUpValid(_Model);
                            return View();

                        }
                    }



                    var UserAPI = client.PostAsJsonAsync<SystemUsers>("api/PublicAPI/AddUserAPI", _Model);
                    UserAPI.Wait();
                    var UserResultAPI = UserAPI.Result;
                    if (UserResultAPI.IsSuccessStatusCode)
                    {
                        var UserResults = UserResultAPI.Content.ReadAsStringAsync().Result;
                        int UserlId = JsonConvert.DeserializeObject<int>(UserResults);
                        _Model.UserID = UserlId;
                        SignIn();

                    }

                }

            }
            return RedirectToAction("SignIn", "Users");
        }

        [HttpGet]
        public ActionResult SignUpValid(SystemUsers _model)
        {
            try
            {
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

                        _model.Countries = (from d in CountriesListModel
                                            select new SelectListItem
                                            {
                                                Value = d.CountryID.ToString(),
                                                Text = d.CountryName
                                            }).ToList();
                    }
                }
                return View(_model);
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("Index","Users");
            }
        }


    
        public ActionResult Logout()
        {
            try
            {
                Session["UserProfile"] = null;
                return RedirectToAction("SignIn", "Users");
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("Index", "Users");
            }
        }
        [HttpPost]
        public ActionResult LoggedError(string error)
        {
            var path = @"C:\Logerror.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write("UsersController: " + error);
            }
            return null;
        }
    }

}