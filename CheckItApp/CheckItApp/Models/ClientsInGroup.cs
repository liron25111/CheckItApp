using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class ClientsInGroup
    {
        public int ClientId { get; set; }
        public int GroupId { get; set; }

     
        public virtual Student Client { get; set; }
      
     
        public virtual Class Group { get; set; }
    }
}
