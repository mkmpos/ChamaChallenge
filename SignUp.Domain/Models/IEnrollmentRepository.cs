using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignUp.Domain.Models
{
    public interface IEnrollmentRepository
    {
        Enrollment Add(Enrollment enrollment);

        Task SaveChangesAsync();

        Task<Enrollment> FindAsync(int id);

        IEnumerable GetList();
        IEnumerable GetDetails(int id);

    }
}
