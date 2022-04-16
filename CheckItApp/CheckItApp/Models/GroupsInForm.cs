using System;
using System.Collections.Generic;

namespace CheckItApp.Models
{
    public partial class GroupsInForm
    {
        public int GroupId { get; set; }
        public int FormId { get; set; }
        public virtual Form Form { get; set; }
        public virtual Class Group { get; set; }
    }
}
