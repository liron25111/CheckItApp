using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class SignForm
    {
        public int IdOfForm { get; set; }
       
        public int Account { get; set; }
        public TimeSpan SignatureTime { get; set; }

        
        public virtual Account AccountNavigation { get; set; }
        
        public virtual Form IdOfFormNavigation { get; set; }
    }
}
