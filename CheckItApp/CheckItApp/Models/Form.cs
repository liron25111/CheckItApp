using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class Form
    {
        public Form()
        {
            FormsOfGroups = new List<FormsOfGroup>();
        }

        public string FormType { get; set; }
     
        public string Topic { get; set; }
     
        public string MassageBody { get; set; }
        public int StatusOfTheMessage { get; set; }
        public int Sender { get; set; }
      
        public int FormId { get; set; }
     
        public DateTime TimeSend { get; set; }

      
        public virtual StaffMember SenderNavigation { get; set; }
      
        public virtual Signform Signform { get; set; }
        public virtual List<FormsOfGroup> FormsOfGroups { get; set; }
    }
}
