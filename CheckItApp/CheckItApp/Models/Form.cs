using System;
using System.Collections.Generic;

namespace CheckItApp.Models
{

    public partial class Form
    {
        public Form()
        {
            SignForms = new HashSet<SignForm>();
        }

    
        public string FormType { get; set; }

        public string Topic { get; set; }
  
        public string MassageBody { get; set; }
        public int StatusOfTheMessage { get; set; }

        public int FormId { get; set; }

        public DateTime TripDate { get; set; }
        public int SentByStaffMemebr { get; set; }

        public virtual StaffMember SentByStaffMemebrNavigation { get; set; }

        public virtual ICollection<SignForm> SignForms { get; set; }
    }
}
