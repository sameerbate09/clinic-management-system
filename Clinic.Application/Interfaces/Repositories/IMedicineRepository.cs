using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories;

public interface IMedicineRepository
{
    Task AddAsync(Medicine medicine);
    Task UpdateAsync(Medicine medicine);
    Task<Medicine?> GetByGuidAsync(Guid medicineGuid);
    Task<List<Medicine>> GetAllAsync();
    Task<List<Clinic.Application.DTOs.MedicineSearchDto>> SearchByNameAsync(string term);
    Task<bool> DeleteAsync(Guid medicineId);
}