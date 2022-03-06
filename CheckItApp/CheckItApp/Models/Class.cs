using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class Class
    {
        public Class()
        {
            ClientsInGroups = new List<ClientsInGroup>();
            Forms = new List<Form>();
        }

       
        public string ClassName { get; set; }
        public int StaffMemberOfGroup { get; set; }
     
        public int GroupId { get; set; }
       
        public string ClassYear { get; set; }

        
        public virtual StaffMember StaffMemberOfGroupNavigation { get; set; }
      
        public virtual List<ClientsInGroup> ClientsInGroups { get; set; }
        public virtual List<Form> Forms { get; set; }
    }
}
