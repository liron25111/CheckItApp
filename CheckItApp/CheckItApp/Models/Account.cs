using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{

    public partial class Account
    {
    
        public string Username { get; set; }
    
        public string Pass { get; set; }
      
        public int Id { get; set; }
    
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual StaffMember StaffMember { get; set; }
        public virtual Student Student { get; set; }
    }
}
