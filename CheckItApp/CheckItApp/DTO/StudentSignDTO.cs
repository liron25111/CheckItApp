using CheckItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckItApp.DTO
{
    public class StudentSignDTO
    {
        public Student s;
        public bool signed;
        public StudentSignDTO(Student s, bool b)
        {
            this.s = s;
            signed = b;
        }
        public StudentSignDTO()
        {

        }
    }
}
