using System;
using System.Collections.Generic;




namespace CheckItApp.Models
{
    public partial class AccountOrganization
    {
        
        public int AccountId { get; set; }
    
        public int OrganizationId { get; set; }

     
        public virtual Account Account { get; set; }
      
        public virtual Organization Organization { get; set; }
    }
}
