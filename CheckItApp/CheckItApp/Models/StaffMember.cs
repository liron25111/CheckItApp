using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{

    public partial class StaffMember
    {
        public StaffMember()
        {
            Classes = new HashSet<Class>();
            Forms = new HashSet<Form>();
            Organizations = new HashSet<Organization>();
        }



        public int Id { get; set; }

        public string MemberName { get; set; }
        public int? SchoolId { get; set; }

        public string Pass { get; set; }

        public string Email { get; set; }

        public virtual Organization School { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public virtual ICollection<Form> Forms { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}