using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Models
{
    public class Complaints
    {

        public int ComplaintsID { get; set; }
        public int UserID { get; set; }
        public string DisplaySenderName { get; set; }

        public List<SelectListItem> ComplaintStatus { get; set; }
        [Display(Name = "Complaint Status")]
        public int ComplaintStatusID { get; set; }

        [Display(Name = "Would you like to refund")]
        [Required(ErrorMessage = "Refund Selection Is Required")]
        public bool Refund { get; set; }
        [Display(Name = "Country")]
        public List<SelectListItem> Countries { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country Is Required")]
        public int CountryID { get; set; }

        [Display(Name = "Contact Method")]
        public List<SelectListItem> ContactMethods { get; set; }
        [Display(Name = "Departments")]

        public List<SelectListItem> Departments { get; set; }
        [Display(Name = "Departments")]

        [Required(ErrorMessage = "Departments Is Required")]
        public int[] DepartmentsID { get; set; }

        [Required(ErrorMessage = "Contact Method Is Required")]
        public int ContactMethod { get; set; }
        [Display(Name = "Complaint Details")]

        [Required(ErrorMessage = "Details Is Required")]
        public string Details { get; set; }
        public DateTime ComplaintLoggedTime { get; set; }
    }
}