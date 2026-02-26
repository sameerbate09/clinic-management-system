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