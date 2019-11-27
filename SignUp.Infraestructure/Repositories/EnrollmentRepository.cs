using Microsoft.EntityFrameworkCore;
using SignUp.API.Models;
using SignUp.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUp.Infraestructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly EnrollmentContext _context;

        public EnrollmentRepository(EnrollmentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Enrollment Add(Enrollment enrollment)
        {
            try
            {
                //check if the course is open
                List<Course> list = _context.Courses.ToList();
                Course _course = _context.Courses.Find(enrollment.CourseID);
                if (_course != null)
                {
                    if (_course.IsOpen)
                    {
                        return _context.Enrollments.Add(enrollment).Entity;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Enrollment> FindAsync(int id)
        {
            return await _context.Enrollments.FindAsync(id);
        }

        public IEnumerable GetList()
        {
            var result = _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .AsNoTracking()
                .Select(c => new
                {
                    c.Title,
                    c.MaxPaticipants,
                    TotalStudent = c.Enrollments.Count(),
                    MaxAge = c.Enrollments.Max(e => e.Student.Age),
                    MinAge = c.Enrollments.Min(e => e.Student.Age),
                    AvgAge = (int)c.Enrollments.Average(e => e.Student.Age)
                });

            return result;      
        }

        public IEnumerable GetDetails(int courseId)
        {
            var courseDetail = _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .AsNoTracking()
                .Where(c => c.CourseID == courseId)
                .Select(c => new
                {
                    title = c.Title,
                    teacher = c.Teacher.Name + " " + c.Teacher.LastName,
                    students = c.Enrollments.Select(x => new 
                    { 
                        name = x.Student.FirstName + " " + x.Student.LastName, 
                        age = x.Student.Age, 
                        mail = x.Student.Mail 
                    })
                });
                

            return courseDetail;
        }
    }
}
