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
    public class ComplaintsController : Controller
    {
        string Baseurl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/";
        [HttpGet]
        public ActionResult SendComplaint()
        {
            try
            {
                LogginUser userLogin = GetUser();
                if(userLogin == null || userLogin.UserTypeID != 1)
                {
                        return RedirectToAction("SignIn", "Users");
                    
                }
                else
                {
                    ViewBag.name = userLogin.FullName;
                    ViewBag.Usertype = userLogin.UserTypeID;

                }

                Complaints _model = new Complaints();
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
                    List<ContactMethod> ContactMethodstModel = null;
                    var MethodsListAPI = client.GetAsync("api/PublicAPI/GetMethodsAPI/").Result;
                    if (MethodsListAPI.IsSuccessStatusCode)
                    {
                        var MethodsListIDs = MethodsListAPI.Content.ReadAsStringAsync().Result;

                        ContactMethodstModel = JsonConvert.DeserializeObject<List<ContactMethod>>(MethodsListIDs);


                        _model.ContactMethods = (from d in ContactMethodstModel
                                                select new SelectListItem
                                                {
                                                    Value = d.ContactMethodID.ToString(),
                                                    Text = d.Name
                                                }).ToList();
                    }

                    List<Departments> DepartmentstModel = null;
                    var DepartmentsListAPI = client.GetAsync("api/PublicAPI/GetDepartmentsAPI/").Result;
                    if (DepartmentsListAPI.IsSuccessStatusCode)
                    {
                        var DeparmentListIDs = DepartmentsListAPI.Content.ReadAsStringAsync().Result;

                        DepartmentstModel = JsonConvert.DeserializeObject<List<Departments>>(DeparmentListIDs);


                        _model.Departments = (from d in DepartmentstModel
                                              select new SelectListItem
                                              {
                                                  Value = d.DepartmentID.ToString(),
                                                  Text = d.Name.ToString()
                                              }).ToList();
                    }

                }



                return View(_model);
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("Users", "Index");
            }
        }

        [HttpPost]
        public ActionResult SendComplaint(Complaints Model)
        {
            try
            {
                LogginUser userLogin = GetUser();
                if (userLogin == null || userLogin.UserTypeID != 1)
                {
                    return RedirectToAction("SignIn", "Users");

                }
                else
                {
                    ViewBag.name = userLogin.FullName;
                    ViewBag.Usertype = userLogin.UserTypeID;

                }
                if (!ModelState.IsValid)
                {
                    SendComplaintValid(Model);
                    return View();
                }
                else
                {
                    using (var client = new HttpClient())
                    {

                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        Model.ComplaintLoggedTime = DateTime.Now;
                        Model.ComplaintStatusID = 2;
                        Model.UserID = userLogin.UserID;
                        var AddComplaintAPI = client.PostAsJsonAsync<Complaints>("api/PublicAPI/AddComplaintAPI", Model);
                        AddComplaintAPI.Wait();
                        var AddComplaint = AddComplaintAPI.Result;
                        if (AddComplaint.IsSuccessStatusCode)
                        {
                            var Results = AddComplaint.Content.ReadAsStringAsync().Result;
                            int ComplaintID = JsonConvert.DeserializeObject<int>(Results);

                            if (ComplaintID > 0)
                            {
                                foreach (int i in Model.DepartmentsID)
                                {

                                    var API = client.GetAsync(string.Format("api/PublicAPI/AddComplaintDepartmentAPI?Depatment={0}&ComplaintID={1}", i, ComplaintID)).Result;
                                    if (API.IsSuccessStatusCode)
                                    {
                                        var APIResults = API.Content.ReadAsStringAsync().Result;
                                    }

                                 }
                              }
                        }
                    }
                    return RedirectToAction("MyComplaints", "Complaints", new
                    {
                        UserID = userLogin.UserID
                    });
                }
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("Users", "Index");
            }
        }


        private LogginUser GetUser()
        {
            LogginUser result = null;
            if(Session["UserProfile"] != null)
            {
                result = Session["UserProfile"] as LogginUser;
            }

            return result;
        }


        [HttpGet]
        public ActionResult MyComplaints(int ? UserID)
        {
            LogginUser userLogin = GetUser();
            if (userLogin == null || userLogin.UserTypeID != 1)
            {
                return RedirectToAction("SignIn", "Users");

            }
            else
            {
                ViewBag.name = userLogin.FullName;
                ViewBag.Usertype = userLogin.UserTypeID;
                UserID = userLogin.UserID;
            }
            List<ComplaintsGrid> RetrievedComplaints = null;
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Result = client.GetAsync(string.Format("api/PublicAPI/GetMyComplaintsAPI?UserID=" + UserID)).Result;
                if (Result.IsSuccessStatusCode)
                {
                    var MyComplaintResults = Result.Content.ReadAsStringAsync().Result;
                    RetrievedComplaints = JsonConvert.DeserializeObject<List<ComplaintsGrid>>(MyComplaintResults);
                }
            }
            
            return View(RetrievedComplaints);
        }

        [HttpGet]
        public ActionResult SendComplaintValid(Complaints model)
        {
            try
            {
                LogginUser userLogin = GetUser();
                if (userLogin == null || userLogin.UserTypeID != 1)
                {
                    return RedirectToAction("SignIn", "Users");

                }
                else
                {
                    ViewBag.name = userLogin.FullName;
                    ViewBag.Usertype = userLogin.UserTypeID;
                }
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


                        model.Countries = (from d in CountriesListModel
                                            select new SelectListItem
                                            {
                                                Value = d.CountryID.ToString(),
                                                Text = d.CountryName
                                            }).ToList();
                    }
                    List<ContactMethod> ContactMethodstModel = null;
                    var MethodsListAPI = client.GetAsync("api/PublicAPI/GetMethodsAPI/").Result;
                    if (MethodsListAPI.IsSuccessStatusCode)
                    {
                        var MethodsListIDs = MethodsListAPI.Content.ReadAsStringAsync().Result;

                        ContactMethodstModel = JsonConvert.DeserializeObject<List<ContactMethod>>(MethodsListIDs);


                        model.ContactMethods = (from d in ContactMethodstModel
                                                select new SelectListItem
                                                {
                                                    Value = d.ContactMethodID.ToString(),
                                                    Text = d.Name
                                                }).ToList();
                    }

                    List<Departments> DepartmentstModel = null;
                    var DepartmentsListAPI = client.GetAsync("api/PublicAPI/GetDepartmentsAPI/").Result;
                    if (DepartmentsListAPI.IsSuccessStatusCode)
                    {
                        var DeparmentListIDs = DepartmentsListAPI.Content.ReadAsStringAsync().Result;

                        DepartmentstModel = JsonConvert.DeserializeObject<List<Departments>>(DeparmentListIDs);


                        model.Departments = (from d in DepartmentstModel
                                              select new SelectListItem
                                              {
                                                  Value = d.DepartmentID.ToString(),
                                                  Text = d.Name
                                              }).ToList();
                    }

                }

                return View(model);
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("Users", "Index");
            }
        }

        public ActionResult Details(int complaintID)
        {
            LogginUser userLogin = GetUser();
            if (userLogin == null || complaintID == 0)
            {
                return RedirectToAction("SignIn", "Users");

            }
            else
            {
                ViewBag.name = userLogin.FullName;
                ViewBag.Usertype = userLogin.UserTypeID;
            }
            using (var client = new HttpClient())
            {
                var RetrievedComplaints = new  ComplaintDetails();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Result = client.GetAsync(string.Format("api/PublicAPI/GetMyComplaintsDetailsAPI?complainID=" + complaintID)).Result;
                if (Result.IsSuccessStatusCode)
                {
                    var MyComplaintResults = Result.Content.ReadAsStringAsync().Result;
                    RetrievedComplaints = JsonConvert.DeserializeObject<ComplaintDetails>(MyComplaintResults);
                }
                return View(RetrievedComplaints);

            }
            
        }

        [HttpGet]
        public ActionResult AllComplaints()
        {
            LogginUser userLogin = GetUser();
            if (userLogin == null || userLogin.UserTypeID != 2)
            {
                return RedirectToAction("SignIn", "Users");

            }
            else
            {
                ViewBag.name = userLogin.FullName;
                ViewBag.Usertype = userLogin.UserTypeID;
                
            }
            List<ComplaintsGrid> RetrievedComplaints = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Result = client.GetAsync("api/PublicAPI/GeAllComplaintsAPI").Result;
                if (Result.IsSuccessStatusCode)
                {
                    var MyComplaintResults = Result.Content.ReadAsStringAsync().Result;
                    RetrievedComplaints = JsonConvert.DeserializeObject<List<ComplaintsGrid>>(MyComplaintResults);
                }
            }

            return View(RetrievedComplaints);
        }

        [HttpGet]
        public ActionResult Edit(int complaintID )
        {
            LogginUser userLogin = GetUser();
            if (userLogin == null || userLogin.UserTypeID != 2)
            {
                return RedirectToAction("SignIn", "Users");

            }
            else
            {
                ViewBag.name = userLogin.FullName;
                ViewBag.Usertype = userLogin.UserTypeID;

            }
            var RetrievedComplaints = new ComplaintDetails();

            ComplaintDetails _model = new ComplaintDetails();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Result = client.GetAsync(string.Format("api/PublicAPI/GetMyComplaintsDetailsAPI?complainID=" + complaintID)).Result;
                if (Result.IsSuccessStatusCode)
                {
                    var MyComplaintResults = Result.Content.ReadAsStringAsync().Result;
                    RetrievedComplaints = JsonConvert.DeserializeObject<ComplaintDetails>(MyComplaintResults);
                }
                List<ComplaintStatus> ComplaintStatustModel = null;
                var ComplaintStatusListAPI = client.GetAsync("api/PublicAPI/GetComplaintStatusAPI/").Result;
                if (ComplaintStatusListAPI.IsSuccessStatusCode)
                {
                    var ComplaintsStatusListIDs = ComplaintStatusListAPI.Content.ReadAsStringAsync().Result;

                    ComplaintStatustModel = JsonConvert.DeserializeObject<List<ComplaintStatus>>(ComplaintsStatusListIDs);


                    RetrievedComplaints.ComplaintStatus = (from d in ComplaintStatustModel
                                                           select new SelectListItem
                                                           {
                                                               Value = d.ComplaintStatusID.ToString(),
                                                               Text = d.Name.ToString()
                                                           }).ToList();
                }


            }

            return View(RetrievedComplaints);
        }

        [HttpGet]
        public ActionResult ValidateEdit(ComplaintDetails Model)
        {
            LogginUser userLogin = GetUser();
            if (userLogin == null || userLogin.UserTypeID != 2)
            {
                return RedirectToAction("SignIn", "Users");

            }
            else
            {
                ViewBag.name = userLogin.FullName;
                ViewBag.Usertype = userLogin.UserTypeID;

            }
            var RetrievedComplaints = new ComplaintDetails();

            ComplaintDetails _model = new ComplaintDetails();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Result = client.GetAsync(string.Format("api/PublicAPI/GetMyComplaintsDetailsAPI?complainID=" + Model.ComplaintsID)).Result;
                if (Result.IsSuccessStatusCode)
                {
                    var MyComplaintResults = Result.Content.ReadAsStringAsync().Result;
                    RetrievedComplaints = JsonConvert.DeserializeObject<ComplaintDetails>(MyComplaintResults);
                }
                List<ComplaintStatus> ComplaintStatustModel = null;
                var ComplaintStatusListAPI = client.GetAsync("api/PublicAPI/GetComplaintStatusAPI/").Result;
                if (ComplaintStatusListAPI.IsSuccessStatusCode)
                {
                    var ComplaintsStatusListIDs = ComplaintStatusListAPI.Content.ReadAsStringAsync().Result;

                    ComplaintStatustModel = JsonConvert.DeserializeObject<List<ComplaintStatus>>(ComplaintsStatusListIDs);


                    RetrievedComplaints.ComplaintStatus = (from d in ComplaintStatustModel
                                                           select new SelectListItem
                                                           {
                                                               Value = d.ComplaintStatusID.ToString(),
                                                               Text = d.Name.ToString()
                                                           }).ToList();
                }


            }

            return View(RetrievedComplaints);
        }
        [HttpPost]
        public ActionResult Edit(ComplaintDetails Model)
        {
            try
            {
                LogginUser userLogin = GetUser();
                if (userLogin == null || userLogin.UserTypeID != 2)
                {
                    return RedirectToAction("SignIn", "Users");

                }
                else
                {
                    ViewBag.name = userLogin.FullName;
                    ViewBag.Usertype = userLogin.UserTypeID;

                }
                if (!ModelState.IsValid)
                {
                    ValidateEdit(Model);
                    return View();

                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var EditComplaintStatusAPI = client.PostAsJsonAsync<ComplaintDetails>("api/PublicAPI/EditComplaintStatus", Model);
                    EditComplaintStatusAPI.Wait();
                    var EditComplaintStatusResultAPI = EditComplaintStatusAPI.Result;
                    if (EditComplaintStatusResultAPI.IsSuccessStatusCode)
                    {
                        var EditComplaintStatusResult = EditComplaintStatusResultAPI.Content.ReadAsStringAsync().Result;
                        bool EditComplaintStatus = JsonConvert.DeserializeObject<bool>(EditComplaintStatusResult);
                        return RedirectToAction("AllComplaints","Complaints");
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return RedirectToAction("Users", "Index");
            }

        }
        [HttpPost]
        public ActionResult LoggedError(string error)
        {
            var path = @"C:\Logerror.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write("Complaints Controller : " + error);
            }
            return null;
        }
    }


}