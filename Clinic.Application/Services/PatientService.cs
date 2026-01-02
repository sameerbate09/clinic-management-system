using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;

namespace Clinic.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<PatientResponse?> CreateAsync(CreatePatientRequest request)
        {
            var domain = new Patient(request.Name, request.Mobile, request.Age, request.Gender, request.Concern);
            await _repository.AddAsync(domain);

            // Try to fetch by mobile to return created entity with Id
            var created = await _repository.GetByMobileAsync(request.Mobile);
            if (created == null) return null;

            return new PatientResponse { PatientId = created.PatientId, Name = created.Name, Mobile = created.Mobile };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeletePatient(id);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PatientResponse>> GetAllAsync()
        {
            var list = await _repository.GetAllPatients();
            return list.Select(p => new PatientResponse { PatientId = p.PatientId, Name = p.Name, Mobile = p.Mobile }).ToList();
        }

        public async Task<PatientResponse?> GetByIdAsync(Guid id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return null;
            return new PatientResponse { PatientId = p.PatientId, Name = p.Name, Mobile = p.Mobile };
        }

        public async Task<PatientResponse?> GetByMobileAsync(string mobile)
        {
            var p = await _repository.GetByMobileAsync(mobile);
            if (p == null) return null;
            return new PatientResponse { PatientId = p.PatientId, Name = p.Name, Mobile = p.Mobile };
        }

        public async Task<PatientResponse?> UpdateAsync(Guid id, CreatePatientRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            var updatedDomain = new Patient(id, request.Name, request.Mobile, request.Age, request.Gender, request.Concern);
            var updated = await _repository.UpdatePatient(updatedDomain);
            if (updated == null) return null;

            return new PatientResponse { PatientId = updated.PatientId, Name = updated.Name, Mobile = updated.Mobile };
        }

        public Task<PatientResponse?> UpdateAsync(int id, CreatePatientRequest request)
        {
            throw new NotImplementedException();
        }
    }
}