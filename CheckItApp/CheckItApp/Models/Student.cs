using System;
using System.Collections.Generic;



namespace CheckItApp.Models
{
    public partial class Student
    {
        public Student()
        {
            ClientsInGroups = new List<ClientsInGroup>();
        }

      
        public int Id { get; set; }
        public int ParentId { get; set; }
       
        public string Name { get; set; }

       
        public virtual Account Parent { get; set; }
        public virtual List<ClientsInGroup> ClientsInGroups { get; set; }
    }
}
