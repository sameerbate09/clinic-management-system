using Clinic.Application.DTOs;

namespace Clinic.Application.Services;

public interface IMedicineService
{
    Task<MedicineResponse> AddAsync(CreateMedicineRequest request);
    Task UpdateAsync(Guid medicineGuid, MedicineUpdateRequest request);
    Task<MedicineResponse?> GetByGuidAsync(Guid medicineGuid);
    Task<List<MedicineResponse>> GetAllAsync();
    Task<bool> DeleteAsync(Guid medicineId);
    Task<List<MedicineSearchDto>> SearchAsync(string? term);
}