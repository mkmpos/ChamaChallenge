using System;
using System.Collections.Generic;
using System.Text;

namespace SignUp.Domain.Models
{
     public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Mail { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
