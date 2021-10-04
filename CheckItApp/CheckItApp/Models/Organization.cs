using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Classes = new List<Class>();
            StaffMembers = new List<StaffMember>();
        }

       
        public int SchoolId { get; set; }
        public int ManagerId { get; set; }
     
       
        public string OrganizationName { get; set; }
        public int MashovSchoolId { get; set; }
      
        public string MashovPass { get; set; }

      
        public virtual StaffMember Manager { get; set; }
      
        public virtual List<Class> Classes { get; set; }
        public virtual List<StaffMember> StaffMembers { get; set; }
    }
}
