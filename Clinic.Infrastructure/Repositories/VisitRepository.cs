using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly ClinicDbContext _context;

        public VisitRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Visit visit)
        {
            var patientExists = await _context.Patients
                .AnyAsync(p => p.PatientGuid == visit.PatientId);

            if (!patientExists)
                throw new KeyNotFoundException($"Patient not found for Id: {visit.PatientId}");

            var entity = new Persistence.Entities.Visit
            {
                VisitGuid = Guid.NewGuid(),
                PatientGuid = visit.PatientId,
                VisitDate = DateTime.UtcNow,
                Complaint = visit.Complaint,
                Notes = visit.Notes,
                NextFollowUpDate = visit.NextFollowUpDate
            };

            await _context.Visits.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.VisitGuid;
        }


        public async Task<Visit> GetByIdAsync(Guid visitId)
        {
            var v = await _context.Visits
            .FirstOrDefaultAsync(x => x.VisitGuid == visitId);

            if (v == null) return null;

            return new Visit
            {
                VisitId = v.VisitGuid,
                PatientId = v.PatientGuid,
                VisitDate = v.VisitDate,
                Complaint = v.Complaint,
                Notes = v.Notes,
                NextFollowUpDate = v.NextFollowUpDate
            };
        }

        public async Task<IEnumerable<Visit>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.Visits
            .Where(v => v.PatientGuid == patientId)
            .OrderByDescending(v => v.VisitDate)
            .Select(v => new Visit
            {
                VisitId = v.VisitGuid,
                PatientId = v.PatientGuid,
                VisitDate = v.VisitDate,
                Complaint = v.Complaint,
                Notes = v.Notes,
                NextFollowUpDate = v.NextFollowUpDate
            })
            .ToListAsync();
        }
    }
}
