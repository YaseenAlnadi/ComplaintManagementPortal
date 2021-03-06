//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class SystemUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SystemUser()
        {
            this.Complaints = new HashSet<Complaint>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryID { get; set; }
        public int GenderID { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public int UserTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual Country Country { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual SystemUser SystemUsers1 { get; set; }
        public virtual SystemUser SystemUser1 { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
