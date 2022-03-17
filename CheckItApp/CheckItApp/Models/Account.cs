using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountOrganizations = new List<AccountOrganization>();
            SignForms = new List<SignForm>();
        }

        
        public string Username { get; set; }
       
        public string Pass { get; set; }
        public int Id { get; set; }
       
        public string Email { get; set; }
        public bool IsActiveStudent { get; set; }

       
        public virtual Student Student { get; set; }
        public virtual ICollection<AccountOrganization> AccountOrganizations { get; set; }
        public virtual ICollection<SignForm> SignForms { get; set; }
    }
}
