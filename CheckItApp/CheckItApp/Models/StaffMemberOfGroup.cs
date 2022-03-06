using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class StaffMemberOfGroup
    {
     
        public int StaffMemberId { get; set; }
      
        public int GroupId { get; set; }

      
        public virtual Class Group { get; set; }
      
        public virtual StaffMember StaffMember { get; set; }
    }
}
