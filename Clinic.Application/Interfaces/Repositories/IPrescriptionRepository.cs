using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<Guid> CreateAsync(Prescription prescription);
        Task<Prescription?> GetByGuidAsync(Guid prescriptionGuid);
        Task<Prescription?> GetByVisitGuidAsync(Guid visitGuid);
        Task UpdateAsync(Prescription prescription);
        Task AutoFinalizeExpiredAsync();
    }
}
