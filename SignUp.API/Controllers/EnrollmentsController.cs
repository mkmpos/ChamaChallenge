using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SignUp.API.Models;
using SignUp.Domain.Models;
using SignUp.Infraestructure.Events;

namespace SignUp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IEnrollmentsEventService _enrollmentEvents;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository, IEnrollmentsEventService enrollmentsEvents)
        {
            _enrollmentRepository = enrollmentRepository;
            _enrollmentEvents = enrollmentsEvents;
        }

        // GET: api/Enrollments
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollment()
        //{
        //    return await _enrollmentRepository.Enrollment.ToListAsync();
        //}

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _enrollmentRepository.FindAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }

        // PUT: api/Enrollments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment)
        //{
        //    if (id != enrollment.EnrollmentID)
        //    {
        //        return BadRequest();
        //    }

        //    _enrollmentRepository.Entry(enrollment).State = EntityState.Modified;

        //    try
        //    {
        //        await _enrollmentRepository.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EnrollmentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Enrollments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
             Enrollment enrollresult = _enrollmentRepository.Add(enrollment);
            if (enrollresult != null)
            {
                await _enrollmentRepository.SaveChangesAsync();
                return StatusCode((int)System.Net.HttpStatusCode.OK, "Enrollment Success");
            }
            else
            {
                return StatusCode((int)System.Net.HttpStatusCode.Forbidden, "Course Is Full");
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ActionName("PostEnrollmentAsync")]
        public async Task<ActionResult<Enrollment>> PostEnrollmentAsync(Enrollment enrollment)
        {
            await _enrollmentEvents.EnrollASync(enrollment);

            return Ok();
        }

        [ResponseCache(Duration = 36000)]
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetEnrollmentsList")]
        public IActionResult GetEnrollmentsList()
        {
            var result = _enrollmentRepository.GetList();

            return new ObjectResult(result);
        }

        [ResponseCache(Duration = 36000)]
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetCourseDetail")]
        public IActionResult GetCourseDetail(int id)
        {
            var result = _enrollmentRepository.GetDetails(id);

            return new ObjectResult(result);
        }

        //// DELETE: api/Enrollments/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Enrollment>> DeleteEnrollment(int id)
        //{
        //    var enrollment = await _enrollmentRepository.Enrollment.FindAsync(id);
        //    if (enrollment == null)
        //    {
        //        return NotFound();
        //    }

        //    _enrollmentRepository.Enrollment.Remove(enrollment);
        //    await _enrollmentRepository.SaveChangesAsync();

        //    return enrollment;
        //}

        //private bool EnrollmentExists(int id)
        //{
        //    return _enrollmentRepository.Enrollment.Any(e => e.EnrollmentID == id);
        //}
    }
}
