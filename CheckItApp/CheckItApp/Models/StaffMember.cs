using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
 
    public partial class StaffMember
    {
        public StaffMember()
        {
            Classes = new List<Class>();
            Organizations = new List<Organization>();
        }

        
        public int Id { get; set; }
        
        public string MemberName { get; set; }
        public int PositionName { get; set; }
        public int SchoolId { get; set; }
       
        public string Pass { get; set; }
      
        public string Email { get; set; }

      
        public virtual Organization School { get; set; }
        public virtual List<Class> Classes { get; set; }
        public virtual List<Organization> Organizations { get; set; }
    }
}
