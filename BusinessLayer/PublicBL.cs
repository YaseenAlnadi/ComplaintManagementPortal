using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlTypes;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BusinessLayer
{
    public class LoggedinUsers
        
        {
        public int UserTypeID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

    }
    public class ComplaintsGrids
    {
        public int ComplaintsID { get; set; }

        public string DisplaySenderName { get; set; }

        public string ComplaintStatus { get; set; }
        public string Refund { get; set; }

        public DateTime ? ComplaintLoggedTime { get; set; }

        public bool RefundBit { get; set; }
        public string Country { get; set; }
    }
    public class ComplaintDetail
    {
        public int ComplaintsID { get; set; }
        public int UserID { get; set; }
        public string DisplaySenderName { get; set; }
        public string Status { get; set; }
        public int ComplaintStatusID { get; set; }
        public bool RefundBit { get; set; }
        public String Refund { get; set; }
        public string Country { get; set; }
        public int CountryID { get; set; }
        public string Departments { get; set; }
        public int ContactMethod { get; set; }
        public String ContactMethods { get; set; }
        public string Details { get; set; }
        public DateTime? ComplaintLoggedTime { get; set; }
    }
        public class PublicBL : BaseClass
    {
        public List<Country> GetCountries()
        {
            try
            {
                InnerDataContext.Configuration.ProxyCreationEnabled = false;
                List<Country> Result = InnerDataContext.Countries.ToList();
                return Result;

            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return null;
            }
        }

        public List<ContactMethod> GetContactMethods()
        {
            try
            {
                InnerDataContext.Configuration.ProxyCreationEnabled = false;
                List<ContactMethod> Result = InnerDataContext.ContactMethods.ToList();
                return Result;

            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return null;
            }
        }
        public List<Department> GetDepartments()
        {
            try
            {
                InnerDataContext.Configuration.ProxyCreationEnabled = false;
                List<Department> Result = InnerDataContext.Departments.ToList();
                return Result;

            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return null;
            }
        }

        public List<ComplaintStatu> GetComplaintStatus()
        {
            try
            {
                InnerDataContext.Configuration.ProxyCreationEnabled = false;
                List<ComplaintStatu> Result = InnerDataContext.ComplaintStatus.ToList();
                return Result;

            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return null;
            }
        }
        public LoggedinUsers Loggin(SystemUser user)
        {
            try
            {
                var result = new LoggedinUsers();

                InnerDataContext.Configuration.ProxyCreationEnabled = false;
                var userreturn = InnerDataContext.SystemUsers.Where(su => su.UserName.Equals(user.UserName)
                && su.Password.Equals(user.Password)).SingleOrDefault();
                if(userreturn != null)
                {
                    result = new LoggedinUsers { FullName = $"{userreturn.FirstName} {userreturn.LastName}", Password = userreturn.Password, UserID = userreturn.UserID, UserName = userreturn.UserName, UserTypeID = userreturn.UserTypeID };

                }
                return result;

            }
            catch (Exception ex)
            {
                LoggedError(ex.Message);
                return null;
            }
        }

        public int AddUser(SystemUser User)
        {
            try
            {
                SystemUser NewUser = InnerDataContext.SystemUsers.Add(User);
                InnerDataContext.SaveChanges();
                return NewUser.UserID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddComplaint(Complaint Complaint)
        {
            try
            {
                Complaint NewComplaint = InnerDataContext.Complaints.Add(Complaint);
                InnerDataContext.SaveChanges();
                return NewComplaint.ComplaintID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int AddComplaintDepartment(ComplaintsDepartment Department)
        {
            try
            {
                ComplaintsDepartment NewComplaint = InnerDataContext.ComplaintsDepartments.Add(Department);
                InnerDataContext.SaveChanges();
                return NewComplaint.ComplaintID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool CheckIfEamilExsist(string Email)
        {
            try
            {
                var query = (from users in InnerDataContext.SystemUsers
                             where users.Email == Email
                             select users);

                if (query.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool CheckIfUserNameExsist(string UserName)
        {
            try
            {
                var query = (from users in InnerDataContext.SystemUsers
                             where users.UserName == UserName
                             select users);

                if (query.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<ComplaintsGrids> GetMyComplaints(int UserID)
        {
            try
            {
                var query = (from Complaints in InnerDataContext.Complaints
                             join country in InnerDataContext.Countries on Complaints.CountryID equals country.CountryID
                             join status in InnerDataContext.ComplaintStatus on Complaints.ComplaintStatusID equals status.ComplaintStatusID
                             join users in InnerDataContext.SystemUsers on Complaints.UserID equals users.UserID
                             where (Complaints.UserID == UserID)
                             select new ComplaintsGrids
                             {
                                 ComplaintLoggedTime = Complaints.ComplaintLoggedTime,
                                 ComplaintsID = Complaints.ComplaintID,
                                 ComplaintStatus = status.Name,
                                 Country = country.CountryName,
                                 DisplaySenderName= users.FirstName +" "+ users.LastName,
                                 RefundBit = Complaints.Refund
                             }).ToList();

                List<ComplaintsGrids> Result = query;
                
                return Result;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ComplaintsGrids> GetAllComplaints()
        {
            try
            {
                var query = (from Complaints in InnerDataContext.Complaints
                             join country in InnerDataContext.Countries on Complaints.CountryID equals country.CountryID
                             join status in InnerDataContext.ComplaintStatus on Complaints.ComplaintStatusID equals status.ComplaintStatusID
                             join users in InnerDataContext.SystemUsers on Complaints.UserID equals users.UserID
                             
                             select new ComplaintsGrids
                             {
                                 ComplaintLoggedTime = Complaints.ComplaintLoggedTime,
                                 ComplaintsID = Complaints.ComplaintID,
                                 ComplaintStatus = status.Name,
                                 Country = country.CountryName,
                                 DisplaySenderName = users.FirstName + " " + users.LastName,
                                 RefundBit = Complaints.Refund
                             }).ToList();

                List<ComplaintsGrids> Result = query;

                return Result;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public ComplaintDetail GetMyComplaintsDetails(int complainID)
        {
            try
            {
                var Departments = (from DepartmentStatus in InnerDataContext.ComplaintsDepartments
                                   join Depart in InnerDataContext.Departments on DepartmentStatus.DepartmentsID equals Depart.DepartmentID
                                   where (DepartmentStatus.ComplaintID == complainID)
                                   select (Depart.Name)
                                   ).Distinct().ToList();


                var query = (from Complaints in InnerDataContext.Complaints
                             join country in InnerDataContext.Countries on Complaints.CountryID equals country.CountryID
                             join status in InnerDataContext.ComplaintStatus on Complaints.ComplaintStatusID equals status.ComplaintStatusID
                             join users in InnerDataContext.SystemUsers on Complaints.UserID equals users.UserID
                             join ContactMethod in InnerDataContext.ContactMethods on Complaints.ContactMethod equals ContactMethod.ContactMethodID

                             where (Complaints.ComplaintID == complainID)
                             select new ComplaintDetail
                             {
                                 ComplaintLoggedTime = Complaints.ComplaintLoggedTime,
                                 ComplaintsID = Complaints.ComplaintID,
                                 Status = status.Name,
                                 Country = country.CountryName,
                                 DisplaySenderName = users.FirstName + " " + users.LastName,
                                 RefundBit = Complaints.Refund,
                                 ContactMethods= ContactMethod.Name,
                                 Details= Complaints.Details,
                                 ComplaintStatusID=Complaints.ComplaintStatusID
                                 
                             }).ToList();

                ComplaintDetail Result = query.SingleOrDefault();
                Result.Departments = string.Join(", ", Departments);
                return Result;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public bool EditComplaintStatus(Complaint complaint)
        {
            try
            {
                Complaint OldStatus = InnerDataContext.Complaints.FirstOrDefault(x => x.ComplaintID == complaint.ComplaintID );

                OldStatus.ComplaintLoggedTime = OldStatus.ComplaintLoggedTime;
                OldStatus.CountryID = OldStatus.CountryID;
                OldStatus.UserID = OldStatus.UserID;
                OldStatus.ContactMethod = OldStatus.ContactMethod;
                OldStatus.Details = OldStatus.Details;
                OldStatus.ComplaintStatusID = complaint.ComplaintStatusID;

                InnerDataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
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
