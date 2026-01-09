using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.DTOs;

namespace Clinic.Application.Services
{
    public interface IVisitService
    {
        Task<Guid> AddAsync(CreateVisitDto dto);
        Task<IEnumerable<VisitSummaryDto>> GetByPatientIdAsync(Guid patientId);
        Task<VisitResponseDto?> GetByIdAsync(Guid visitId);
    }
}
