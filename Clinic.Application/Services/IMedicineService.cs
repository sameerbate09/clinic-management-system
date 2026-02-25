using Clinic.Application.DTOs;

namespace Clinic.Application.Services
{
    public interface IMedicineService
    {
        Task<MedicineResponse> AddAsync(CreateMedicineRequest request);
        Task UpdateAsync(MedicineUpdateRequest request);
        Task<List<MedicineResponse>> GetAllAsync();
        Task<bool> DeleteAsync(Guid medicineId);
    }
}