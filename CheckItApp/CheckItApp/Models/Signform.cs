﻿using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
   
    public partial class Signform
    {
       
        public int IdOfForm { get; set; }
        public int PerentSignId { get; set; }
        public int GroupId { get; set; }
      
        public DateTime SignTime { get; set; }

      
        public virtual Form IdOfFormNavigation { get; set; }
    }
}
