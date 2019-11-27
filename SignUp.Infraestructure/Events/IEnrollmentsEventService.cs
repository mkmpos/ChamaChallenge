using SignUp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignUp.Infraestructure.Events
{
    public interface IEnrollmentsEventService
    {
        Task EnrollASync(Enrollment enrollment);
    }
}
