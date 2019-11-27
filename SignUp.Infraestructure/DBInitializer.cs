using SignUp.API.Models;
using SignUp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignUp.Infraestructure
{
    public static class DBInitializer
    {
        public static void Initialize(EnrollmentContext context)
        {
            context.Database.EnsureCreated();

            // Look for enrollment.
            if (context.Enrollments.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
{
                new Student{StudentID=1, Age=23, FirstName="Lebron", LastName="James", Mail="student1@gg.com"},
                new Student{StudentID=2, Age=25, FirstName="Pablo", LastName="Aimar", Mail="student2@gg.com"},
                new Student{StudentID=3, Age=27, FirstName="Pablo", LastName="Escobar", Mail="student3@gg.com"},
                new Student{StudentID=4, Age=29, FirstName="Estrella", LastName="Torres", Mail="student4@gg.com"},
                new Student{StudentID=5, Age=21, FirstName="Karen", LastName="Alga", Mail="student5@gg.com"}
};
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var teachers = new Teacher[]
            {
                new Teacher{TeacherID=1, Name="Marcelo", LastName="Bielsa"},
                new Teacher{TeacherID=2, Name="Erwe", LastName="Von Esse"},
                new Teacher{TeacherID=3, Name="Ruben", LastName="Aguirre"}
            };
            foreach (Teacher t in teachers)
            {
                context.Teachers.Add(t);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Chemistry", IsOpen=true, MaxPaticipants=10, TeacherID=1},
                new Course{CourseID=4022,Title="Microeconomics", IsOpen=false, MaxPaticipants=10, TeacherID=2},
                new Course{CourseID=1045,Title="Calculus", IsOpen=true, MaxPaticipants=5, TeacherID=3},
                new Course{CourseID=3141,Title="Trigonometry", IsOpen=true, MaxPaticipants=10, TeacherID=1},
                new Course{CourseID=2021,Title="Composition", IsOpen=true, MaxPaticipants=10, TeacherID=2},
                new Course{CourseID=2042,Title="Literature", IsOpen=true, MaxPaticipants=10, TeacherID=3}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
           {
                new Enrollment{StudentID=1,CourseID=1050},
                new Enrollment{StudentID=1,CourseID=4022},
                new Enrollment{StudentID=1,CourseID=1045},
                new Enrollment{StudentID=2,CourseID=1045},
                new Enrollment{StudentID=2,CourseID=3141},
                new Enrollment{StudentID=2,CourseID=2021},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=3,CourseID=2042},
                new Enrollment{StudentID=4,CourseID=4022},
                new Enrollment{StudentID=4,CourseID=1045},
                new Enrollment{StudentID=5,CourseID=2021},
                new Enrollment{StudentID=5,CourseID=3141},
           };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}
