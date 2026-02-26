using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;

namespace Clinic.Application.Services;

public class MedicineService : IMedicineService
{
    private readonly IMedicineRepository _medicineRepository;
    public MedicineService(IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }
   
    public async Task<MedicineResponse> AddAsync(CreateMedicineRequest request)
    {
        var domain = new Medicine(request.Name, request.Description);
        await _medicineRepository.AddAsync(domain);

        return new MedicineResponse
        {
            MedicineGuid = domain.MedicineGuid,
            Name = domain.Name,
            Description = domain.Description
        };
    }

    public Task<bool> DeleteAsync(Guid medicineId)
    {
        return _medicineRepository.DeleteAsync(medicineId);
    }

    public async Task<List<MedicineResponse>> GetAllAsync()
    {
        var list = await _medicineRepository.GetAllAsync();
        return list.Select(m => new MedicineResponse { MedicineGuid = m.MedicineGuid, Name = m.Name, Description = m.Description }).ToList();
    }

    public async Task UpdateAsync(Guid medicineGuid, MedicineUpdateRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var existing = await _medicineRepository.GetByGuidAsync(medicineGuid);
        if (existing == null)
            throw new InvalidOperationException("Medicine not found");

        var medicine = new Medicine(medicineGuid, request.Name, request.Description);

        await _medicineRepository.UpdateAsync(medicine);
    }

    public async Task<MedicineResponse?> GetByGuidAsync(Guid medicineGuid)
    {
        var m = await _medicineRepository.GetByGuidAsync(medicineGuid);
        if (m == null) return null;
        return new MedicineResponse { MedicineGuid = m.MedicineGuid, Name = m.Name, Description = m.Description };
    }

    public async Task<List<MedicineSearchDto>> SearchAsync(string? term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return new List<MedicineSearchDto>();

        // Repository already projects and filters active records
        var results = await _medicineRepository.SearchByNameAsync(term!);
        return results;
    }
}