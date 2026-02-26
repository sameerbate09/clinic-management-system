using Clinic.Application.DTOs;

namespace Clinic.Application.Services;

public interface IPrescriptionService
{
    Task<Guid> CreateAsync(CreatePrescriptionRequest request);
    Task<PrescriptionResponseDto?> GetAsync(Guid prescriptionGuid);
    Task<PrescriptionResponseDto?> GetByVisitGuidAsync(Guid visitGuid);
    Task UpdateAsync(Guid prescriptionGuid, UpdatePrescriptionRequest request);
    Task AutoFinalizeExpiredAsync();
}