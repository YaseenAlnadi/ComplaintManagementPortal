using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintManagementPortalProject.Models
{
    public class Complaints
    {

        public int ComplaintsID { get; set; }
        public int UserID { get; set; }
        public string DisplaySenderName { get; set; }

        public List<SelectListItem> ComplaintStatus { get; set; }

        public int ComplaintStatusID { get; set; }

        [Required(ErrorMessage = "Refund Selection Is Required")]
        public bool Refund { get; set; }

        public List<SelectListItem> Countries { get; set; }

        [Required(ErrorMessage = "Country Is Required")]
        public int CountryID { get; set; }

        public List<SelectListItem> ContactMethod { get; set; }

        [Required(ErrorMessage = "ContractMethod Is Required")]
        public int ContactMethodID { get; set; }

        [Required(ErrorMessage = "Details Is Required")]
        public string Details { get; set; }
        public DateTime ComplaintLoggedTime { get; set; }
    }
}