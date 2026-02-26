using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using InfraEntity = Clinic.Infrastructure.Persistence.Entities.Patient;

namespace Clinic.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ClinicDbContext _context;

    public PatientRepository(ClinicDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(Patient patient)
    {
        if (patient is null)
            throw new ArgumentNullException(nameof(patient), "Patient cannot be null");

        // Map domain Patient to persistence entity
        var entity = new InfraEntity
        {
            Name = patient.Name,
            Mobile = patient.Mobile,
            Age = patient.Age,
            Gender = patient.Gender,
            Concern = patient.Concern,
            // IsActive and CreatedDate are configured with defaults in the DbContext model;
            // leave them unset so the database defaults are applied.
        };

        await _context.Patients.AddAsync(entity);
        await _context.SaveChangesAsync();

        // Domain Patient has private setters; repository currently does not return the created domain object.
    }

    public async Task<bool> DeletePatient(Guid patientId)
    {
        var entity = await _context.Patients.FirstOrDefaultAsync(p => p.PatientGuid == patientId);
        if (entity == null)
            return false;

        // Soft-delete: mark inactive
        entity.IsActive = false;
        _context.Patients.Update(entity);
        await _context.SaveChangesAsync();
        return true; 
    }

    public async Task<List<Patient>> GetAllPatients()
    {
        var list = await _context.Patients
            .Where(p => p.IsActive)
            .AsNoTracking()
            .ToListAsync();

        return list.Select(MapToDomain).ToList();
    }

    public async Task<Patient> GetByIdAsync(Guid patientId)
    {
        var entity = await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PatientGuid == patientId && p.IsActive);

        if (entity == null)
            return null!; // caller should handle null (interface expects Task<Patient>)

        return MapToDomain(entity);
    }

    public async Task<Patient> GetByMobileAsync(string mobile)
    {
        if (string.IsNullOrWhiteSpace(mobile))
            throw new ArgumentException("Mobile cannot be null or empty", nameof(mobile));

        // Normalize input to avoid leading/trailing space mismatches
        var normalizedMobile = mobile.Trim();

        var entity = await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Mobile == normalizedMobile && p.IsActive);

        if (entity == null)
            return null!;

        return MapToDomain(entity);
    }

    public async Task<Patient> UpdatePatient(Patient patient)
    {
        if (patient == null) throw new ArgumentNullException(nameof(patient));

        var entity = await _context.Patients.FirstOrDefaultAsync(p => p.PatientGuid == patient.PatientId && p.IsActive);
        if (entity == null) return null!;

        // Apply updates from domain patient to persistence entity
        entity.Name = patient.Name;
        entity.Mobile = patient.Mobile;
        entity.Age = patient.Age;
        entity.Gender = patient.Gender;
        entity.Concern = patient.Concern;

        _context.Patients.Update(entity);
        await _context.SaveChangesAsync();

        return MapToDomain(entity);
    }

    private static Patient MapToDomain(InfraEntity entity)
    {
        // Map persistence entity to domain entity. Domain requires non-null values.
        var age = entity.Age ?? 0;
        var gender = entity.Gender ?? string.Empty;
        var concern = entity.Concern ?? string.Empty;

        return new Patient(entity.PatientGuid, entity.Name ?? string.Empty, entity.Mobile ?? string.Empty, age, gender, concern);
    }
}