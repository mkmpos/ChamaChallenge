using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignUp.Domain.Models;

namespace SignUp.API.Models
{
    public class EnrollmentContext : DbContext
    {
        public EnrollmentContext (DbContextOptions<EnrollmentContext> options)
            : base(options)
        {
        }

        public DbSet<SignUp.Domain.Models.Enrollment> Enrollments { get; set; }
        public DbSet<SignUp.Domain.Models.Course> Courses { get; set; }
        public DbSet<SignUp.Domain.Models.Student> Students { get; set; }
        public DbSet<SignUp.Domain.Models.Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().HasMany(student => student.Enrollments)
            //    .WithOne().HasForeignKey(enroll => enroll.StudentID);
                
            
            //modelBuilder.Entity<Course>().HasMany(course => course.Enrollments)
            //    .WithOne().HasForeignKey(enroll => enroll.CourseID);

            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");

        }
    }
}
