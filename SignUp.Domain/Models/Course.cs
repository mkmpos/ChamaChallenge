using System;
using System.Collections.Generic;
using System.Text;

namespace SignUp.Domain.Models
{
    public class Course
    {
       public int CourseID { get; set; }
        public string Title { get; set; }

        public int MaxPaticipants { get; set; }

        public bool IsOpen { get; set; }

        public int TeacherID { get; set; }

        public Teacher Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
