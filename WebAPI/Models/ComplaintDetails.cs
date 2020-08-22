using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Models
{
    public class ComplaintDetails
    {
        public int ComplaintsID { get; set; }
        public int UserID { get; set; }

        [Display(Name = "Customer Name")]
        public string DisplaySenderName { get; set; }

        [Display(Name = "Complaint Status")]
        public List<SelectListItem> ComplaintStatus { get; set; }

        [Required(ErrorMessage = "Complaint Status Is Required")]
        public int ComplaintStatusID { get; set; }
        [Display(Name = "Complaint Status")]
        public string Status { get; set; }

        public bool RefundBit { get; set; }

        [Display(Name = "Would you like to refund")]
        public String Refund { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Display(Name = "Departments")]
        public string Departments { get; set; }
        [Display(Name = "Contact Method")]

        public int ContactMethod { get; set; }
        [Display(Name = "Contact Method")]
        public String ContactMethods { get; set; }

        [Display(Name = "Complaint Details")]
        public string Details { get; set; }

        [Display(Name = "Date of Submission")]
        public DateTime ComplaintLoggedTime { get; set; }
    }
}