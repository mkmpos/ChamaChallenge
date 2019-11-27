using System;
using System.Collections.Generic;
using System.Text;

namespace SignUp.Domain.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int StudentID { get; set; }

        public int CourseID { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
