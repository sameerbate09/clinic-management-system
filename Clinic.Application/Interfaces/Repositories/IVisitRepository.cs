using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories
{
    public interface IVisitRepository
    {
        Task<IEnumerable<Visit>> GetByPatientIdAsync(Guid patientId);
        Task<Visit> GetByIdAsync(Guid visitId);
        Task<Guid> AddAsync(Visit visit);
    }

}
