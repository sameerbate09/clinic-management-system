using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(Guid patientId);
        Task<Patient> GetByMobileAsync(string mobile);
        Task<List<Patient>> GetAllPatients();
        Task<Patient> UpdatePatient(Patient patient);
        Task<bool> DeletePatient(Guid patientId); 
        Task AddAsync(Patient patient);
    }
}
