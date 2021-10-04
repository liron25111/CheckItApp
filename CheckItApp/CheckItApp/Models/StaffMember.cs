using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class StaffMember
    {
        public StaffMember()
        {
            Forms = new List<Form>();
            Organizations = new List<Organization>();
            StaffMemberOfGroups = new List<StaffMemberOfGroup>();
        }

     
        public int Id { get; set; }
     
        public string MemberName { get; set; }
        public int PositionName { get; set; }
        public int SchoolId { get; set; }

       
        public virtual Account IdNavigation { get; set; }
       
        public virtual Organization School { get; set; }
      
        public virtual List<Form> Forms { get; set; }
   
        public virtual List<Organization> Organizations { get; set; }
        public virtual List<StaffMemberOfGroup> StaffMemberOfGroups { get; set; }
    }
}
