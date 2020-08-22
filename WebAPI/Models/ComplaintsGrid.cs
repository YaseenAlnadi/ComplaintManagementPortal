using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ComplaintsGrid
    {
        public int ComplaintsID { get; set; }

        [Display(Name = "Customer Name ")]
        public string DisplaySenderName { get; set; }

        [Display(Name = "Complaint Status")]
        public string ComplaintStatus { get; set; }

        [Display(Name = "Refund Request")]
        public string Refund { get; set; }

        public bool RefundBit { get; set; }

        [Display(Name = "Time of submission")]
        public DateTime ComplaintLoggedTime { get; set; }

        [Display(Name = "Country")]
       public string Country { get; set; }
    }
}