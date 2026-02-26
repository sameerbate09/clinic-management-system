using Clinic.Application.DTOs;

namespace Clinic.Application.Services;

public interface IPatientService
{
    Task<PatientResponse?> GetByIdAsync(Guid id);
    Task<PatientResponse?> GetByMobileAsync(string mobile);
    Task<List<PatientResponse>> GetAllAsync();
    Task<PatientResponse?> CreateAsync(CreatePatientRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<PatientResponse?> UpdateAsync(Guid id, CreatePatientRequest request);
}