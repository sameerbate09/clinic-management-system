using Clinic.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services
{
    public interface IPrescriptionService
    {
        Task<Guid> CreateAsync(CreatePrescriptionRequest request);
        Task<PrescriptionResponseDto?> GetAsync(Guid prescriptionGuid);
        Task<PrescriptionResponseDto?> GetByVisitGuidAsync(Guid visitGuid);
        Task UpdateAsync(Guid prescriptionGuid, UpdatePrescriptionRequest request);
        Task AutoFinalizeExpiredAsync();
    }
}
