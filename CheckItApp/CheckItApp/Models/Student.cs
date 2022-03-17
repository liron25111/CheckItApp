using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class Student
    {
        public Student()
        {
            ClientsInGroups = new HashSet<ClientsInGroup>();
        }

        
        public int Id { get; set; }
       
        public string Name { get; set; }

       
        public virtual Account IdNavigation { get; set; }
      
        public virtual ICollection<ClientsInGroup> ClientsInGroups { get; set; }
    }
}
