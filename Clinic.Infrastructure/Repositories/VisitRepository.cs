using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using InfraEntity = Clinic.Infrastructure.Persistence.Entities.Visit;

namespace Clinic.Infrastructure.Repositories;

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
            Notes = visit.Notes
        };

        await _context.Visits.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.VisitGuid;
    }


    public async Task<Visit> GetByIdAsync(Guid visitId)
    {
        var v = await _context.Visits
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.VisitGuid == visitId);

        if (v == null)
            return null!;

        return MapToDomain(v);
    }

    public async Task<IEnumerable<Visit>> GetByPatientIdAsync(Guid patientId)
    {
        var list = await _context.Visits
            .Where(v => v.PatientGuid == patientId)
            .OrderByDescending(v => v.VisitDate)
            .AsNoTracking()
            .ToListAsync();

        return list.Select(MapToDomain).ToList();
    }

    public async Task<int> GetVisitsCountByDateAsync(DateTime date)
    {
        return await _context.Visits
        .CountAsync(v => v.VisitDate.Date == date.Date);
    }

    public async Task<List<RecentVisitDto>> GetRecentVisitsAsync(int count)
    {
        return await _context.Visits
            .OrderByDescending(v => v.VisitDate)
            .Take(count)
            .Select(v => new RecentVisitDto
            {
                PatientName = v.Patient.Name,
                VisitDate = v.VisitDate,
                Complaint = v.Notes 
            })
            .ToListAsync();
    }

    private static Visit MapToDomain(InfraEntity entity)
    {
        // Map persistence entity to domain entity. Handle possible nulls defensively.
        var notes = entity.Notes ?? string.Empty;
        var complaint = entity.Complaint ?? string.Empty;

        return new Visit
        {
            VisitId = entity.VisitGuid,
            PatientId = entity.PatientGuid,
            VisitDate = entity.VisitDate,
            Complaint = complaint,
            Notes = notes
        };
    }
}