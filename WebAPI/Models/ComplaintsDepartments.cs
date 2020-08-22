using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ComplaintsDepartments
    {
        public int ComplaintsDepartmentsID { get; set; }
        public int ComplaintID { get; set; }
        public int DepartmentsID { get; set; }

    }
}