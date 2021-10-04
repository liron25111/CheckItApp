using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class FormsOfGroup
    {
        public int IdOfGroup { get; set; }
       
        public int FormId { get; set; }

       
        public virtual Form Form { get; set; }
   
        public virtual Class IdOfGroupNavigation { get; set; }
    }
}
