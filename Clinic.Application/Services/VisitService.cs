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
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;

        public VisitService(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }
        public async Task<Guid> AddAsync(CreateVisitDto dto)
        {
            var visit = new Visit(
           dto.PatientId,
           DateTime.UtcNow,
           dto.Complaint,
           dto.Notes,
           dto.NextFollowUpDate
       );

            return await _visitRepository.AddAsync(visit);
        }

        public async Task<VisitResponseDto?> GetByIdAsync(Guid visitId)
        {
            var visit = await _visitRepository.GetByIdAsync(visitId);
            if (visit == null) return null;

            return new VisitResponseDto
            {
                VisitId = visit.VisitId,
                VisitDate = visit.VisitDate,
                Complaint = visit.Complaint,
                Notes = visit.Notes,
                NextFollowUpDate = visit.NextFollowUpDate
            };
        }

        public async Task<IEnumerable<VisitSummaryDto>> GetByPatientIdAsync(Guid patientId)
        {
            var visits = await _visitRepository.GetByPatientIdAsync(patientId);

            return visits.Select(v => new VisitSummaryDto
            {
                VisitId = v.VisitId,
                VisitDate = v.VisitDate,
                NextFollowUpDate = v.NextFollowUpDate
            });
        }
    }
}
