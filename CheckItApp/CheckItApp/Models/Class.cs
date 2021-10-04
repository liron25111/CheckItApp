using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
  
    public partial class Class
    {
        public Class()
        {
            ClientsInGroups = new List<ClientsInGroup>();
            FormsOfGroups = new List<FormsOfGroup>();
            StaffMemberOfGroups = new List<StaffMemberOfGroup>();
        }

      
        public string ClassName { get; set; }
      
        public int GroupId { get; set; }
        public int SchoolId { get; set; }
        public int YearTime { get; set; }

       
        public virtual Organization School { get; set; }
      
        public virtual List<ClientsInGroup> ClientsInGroups { get; set; }
        
        public virtual List<FormsOfGroup> FormsOfGroups { get; set; }
        public virtual List< StaffMemberOfGroup> StaffMemberOfGroups { get; set; }
    }
}
