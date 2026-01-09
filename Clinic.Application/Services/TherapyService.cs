using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;

namespace Clinic.Application.Services
{

    public class TherapyService : ITherapyService
    {
        private readonly ITherapyRepository _repository;

        public TherapyService(ITherapyRepository repository)
        {
            _repository = repository;
        }
       
        public async Task<TherapyResponse> AddTherapyAsync(CreateTherapyRequest request)
        {
            var domain = new Therapy(request.Name); // Use 0 or a default value for Id if it will be set by the database
            await _repository.AddAsync(domain);

            return new TherapyResponse { Id = domain.Id, Name = domain.Name};
        }

        public async Task<List<TherapyResponse>> GetAllTherapiesAsync()
        {
            var list = await _repository.GetAllAsync();
            // Since Therapy does not have IsActive, set IsActive to a default value (e.g., false)
            return list.Select(p => new TherapyResponse { Id = p.Id, Name = p.Name}).ToList();
        }

        public async Task UpdateTherapyAsync(UpdateTherapyRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Map UpdateTherapyRequest to Therapy entity
            var therapy = new Therapy(request.Id, request.Name);
            await _repository.UpdateAsync(therapy);
        }
        public async Task DeleteTherapyAsync(int therapyId)
        {
            var deleted = await _repository.DeleteAsync(therapyId);
            if (!deleted)
                throw new InvalidOperationException("Therapy not found");
        }

        public async Task ReactivateTherapyAsync(int therapyId)
        {
            var reactivated = await _repository.ReactivateAsync(therapyId);
            if (!reactivated)
                throw new InvalidOperationException("Therapy not found or could not be reactivated");
        }
    }
}
