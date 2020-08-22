using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DataAcessLayer;
using Newtonsoft.Json;
using WebAPI.Models;
using ComplaintManagementPortalProject.Models;
using Complaints = WebAPI.Models.Complaints;

namespace WebAPI.Controllers
{
    public class PublicAPIController : ApiController
    {


        public List<Country> GetCountriesAPI()
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    var countires = publicBL.GetCountries();
                    return countires;
                }
            }
            catch (Exception ex)
            {
                // LoggedError(ex.Message);
                return null;
            }
        }

        public List<DataAcessLayer.ContactMethod> GetMethodsAPI()
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    var ContactMethods = publicBL.GetContactMethods();
                    return ContactMethods;
                }
            }
            catch (Exception ex)
            {
                // LoggedError(ex.Message);
                return null;
            }
        }

        public List<Department> GetDepartmentsAPI()
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    var Departments = publicBL.GetDepartments();
                    return Departments;
                }
            }
            catch (Exception ex)
            {
                // LoggedError(ex.Message);
                return null;
            }
        }

        public List<ComplaintStatu> GetComplaintStatusAPI()
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    var ComplaintStatus = publicBL.GetComplaintStatus();
                    return ComplaintStatus;
                }
            }
            catch (Exception ex)
            {
                // LoggedError(ex.Message);
                return null;
            }
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public int AddUserAPI(SystemUser Model)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    SystemUser User = new SystemUser();
                    User.FirstName = Model.FirstName;
                    User.LastName = Model.LastName;
                    User.UserName = Model.UserName;
                    User.Email = Model.Email;
                    User.Password = Model.Password;
                    User.UserTypeID = 1;
                    User.PhoneNumber = Model.PhoneNumber;
                    User.CountryID = Model.CountryID;
                    User.DateOfBirth = Model.DateOfBirth;
                    User.GenderID = Model.GenderID;

                    return publicBL.AddUser(User);
                }
            }
            catch (Exception ex)
            {
                
                return 0;
            }
        }

        public int AddComplaintAPI(Complaint Model)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    Complaint Complaint = new Complaint();
                    Complaint.UserID = Model.UserID;
                    Complaint.ComplaintLoggedTime = Model.ComplaintLoggedTime;
                    Complaint.ComplaintStatusID = Model.ComplaintStatusID;
                    Complaint.ContactMethod = Model.ContactMethod;
                    Complaint.CountryID = Model.CountryID;
                    Complaint.Refund = Model.Refund;
                    Complaint.Details = Model.Details;


                    return publicBL.AddComplaint(Complaint);
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]

        public int AddComplaintDepartmentAPI(int Depatment, int ComplaintID)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    ComplaintsDepartment complaintdeparment = new ComplaintsDepartment();
                    complaintdeparment.ComplaintID = ComplaintID;
                    complaintdeparment.DepartmentsID = Depatment;
                    return publicBL.AddComplaintDepartment(complaintdeparment);
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public LogginUser Login(string Username,string Password)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    SystemUser User = new SystemUser();
                    User.UserName = Username;
                    User.Password = Password;
                    var result = publicBL.Loggin(User);
                    return new LogginUser { FullName = result.FullName, Password = result.Password, UserName = result.UserName, UserID = result.UserID, UserTypeID = result.UserTypeID };
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public bool CheckIfEmailExsists(string Email)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    return publicBL.CheckIfEamilExsist(Email);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public bool CheckIfUserNameExsists(string UserName)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    return publicBL.CheckIfUserNameExsist(UserName);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ComplaintsGrids> GetMyComplaintsAPI(int UserID)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    List<ComplaintsGrids> Grid = publicBL.GetMyComplaints(UserID);
                    if(Grid !=  null && Grid.Count > 0)
                    foreach (var item in Grid)
                    {
                        item.Refund = item.RefundBit ? "Yes" : "No";
                    }
                    return Grid;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]

        public List<ComplaintsGrids> GeAllComplaintsAPI()
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    List<ComplaintsGrids> Grid = publicBL.GetAllComplaints();
                    if (Grid != null && Grid.Count > 0)
                        foreach (var item in Grid)
                        {
                            item.Refund = item.RefundBit ? "Yes" : "No";
                        }
                    return Grid;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ComplaintDetail GetMyComplaintsDetailsAPI(int complainID)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    ComplaintDetail Grid = publicBL.GetMyComplaintsDetails(complainID);
                    if (Grid != null  )
                    {
                        Grid.Refund = Grid.RefundBit ? "Yes" : "No";
                    }
                        
                    return Grid;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]

        public bool EditComplaintStatus(ComplaintDetail Model)
        {
            try
            {
                using (PublicBL publicBL = new PublicBL())
                {
                    Complaint complaint = new Complaint
                    {
                    ComplaintStatusID = Model.ComplaintStatusID,
                    ComplaintID=Model.ComplaintsID
                };
                    return publicBL.EditComplaintStatus(complaint);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}