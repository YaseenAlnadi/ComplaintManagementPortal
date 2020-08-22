using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintManagementPortalProject.Models
{
    public class SystemUsers
    {
        
        public int UserID { get; set; }
        [Required(ErrorMessage = "Username Is Required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Is Required")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Coutry")]
        public List<SelectListItem> Coutries { get; set; }

        [Required(ErrorMessage = "Country Is Required")]
        public int CountryID { get; set; }
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender Is Required")]
        public int GenderID { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Of Birth Is Required")]

        public DateTime DateOfBirth { get; set; }

        public int UserTypeID { get; set; }







    }
}