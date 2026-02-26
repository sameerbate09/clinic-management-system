using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ClinicDbContext _context;

    public PrescriptionRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(Prescription domain)
    {
        // Resolve the DB integer PK for the visit using the domain Visit Guid.
        var visitEntity = await _context.Visits.FirstOrDefaultAsync(v => v.VisitGuid == domain.VisitId);
        if (visitEntity == null)
            throw new InvalidOperationException($"Visit with guid {domain.VisitId} not found.");

        var entity = new Infrastructure.Persistence.Entities.Prescription
        {
            PrescriptionGuid = domain.PrescriptionId,
            VisitGuid = domain.VisitId,
            VisitId = visitEntity.VisitId,
            Notes = domain.Notes,
            CreatedDate = domain.CreatedDate,
            NextFollowUpDate = domain.NextFollowUpDate,
            IsFinalized = domain.IsFinalized
        };

        await _context.Prescriptions.AddAsync(entity);
        await _context.SaveChangesAsync();

        // Add Medicines
        foreach (var med in domain.Medicines)
        {
            // Determine medicine id to use. If caller provided an id, prefer it; otherwise look up by name and create if missing.
            Infrastructure.Persistence.Entities.Medicine? medicineEntity = null;

            if (med.MedicineId > 0)
            {
                // If caller provided an existing medicine id, use that medicine and
                // take the canonical name from DB. Do not create a new medicine.
                medicineEntity = await _context.Medicines.FindAsync(med.MedicineId);
                if (medicineEntity == null)
                    throw new InvalidOperationException($"Medicine with id {med.MedicineId} not found.");
            }
            else
            {
                medicineEntity = await _context.Medicines.FirstOrDefaultAsync(m => m.Name == med.MedicineName);
                if (medicineEntity == null)
                {
                    medicineEntity = new Infrastructure.Persistence.Entities.Medicine
                    {
                        MedicineGuid = Guid.NewGuid(),
                        Name = med.MedicineName
                    };

                    await _context.Medicines.AddAsync(medicineEntity);
                    // Save to obtain the generated MedicineId
                    await _context.SaveChangesAsync();
                }
            }

            _context.PrescriptionMedicines.Add(new Infrastructure.Persistence.Entities.PrescriptionMedicine
            {
                PrescriptionId = entity.PrescriptionId,
                PrescriptionGuid = entity.PrescriptionGuid,
                MedicineId = medicineEntity.MedicineId,
                // Always store the medicine name from the resolved Medicine entity.
                MedicineName = medicineEntity.Name,
                Dosage = med.Dosage,
                Frequency = med.Frequency,
                Instructions = med.Instructions,
                Duration = med.DurationDays
            });
        }

        // Add Therapies
        foreach (var therapy in domain.Therapies)
        {
            // Verify therapy exists
            var therapyEntity = await _context.Therapies.FirstOrDefaultAsync(t => t.TherapyId == therapy.TherapyId);
            if (therapyEntity == null)
                throw new InvalidOperationException($"Therapy with id {therapy.TherapyId} not found.");

            _context.PrescriptionTherapies.Add(new Infrastructure.Persistence.Entities.PrescriptionTherapy
            {
                PrescriptionId = entity.PrescriptionId,
                PrescriptionGuid = entity.PrescriptionGuid,
                TherapyId = therapy.TherapyId,
                Notes = therapy.Notes,
                Sessions = therapy.Sessions
            });
        }

        await _context.SaveChangesAsync();
        return domain.PrescriptionId;
    }

    public async Task<Prescription?> GetByGuidAsync(Guid prescriptionGuid)
    {
        var entity = await _context.Prescriptions
            .Include(p => p.PrescriptionMedicinePrescriptions)
            .Include(p => p.PrescriptionTherapyPrescriptions)
            .FirstOrDefaultAsync(p => p.PrescriptionGuid == prescriptionGuid);

        if (entity == null) return null;

        return MapToDomain(entity);
    }

    public async Task<Prescription?> GetByVisitGuidAsync(Guid visitGuid)
    {
        var entity = await _context.Prescriptions
            .Include(p => p.PrescriptionMedicinePrescriptions)
            .Include(p => p.PrescriptionTherapyPrescriptions)
            .FirstOrDefaultAsync(p => p.VisitGuid == visitGuid);

        if (entity == null) return null;

        return MapToDomain(entity);
    }

    public async Task UpdateAsync(Prescription domain)
    {
        var entity = await _context.Prescriptions
            .Include(p => p.PrescriptionMedicinePrescriptions)
            .Include(p => p.PrescriptionTherapyPrescriptions)
            .FirstOrDefaultAsync(p => p.PrescriptionGuid == domain.PrescriptionId);

        if (entity == null) return;

        entity.Notes = domain.Notes;
        entity.IsFinalized = domain.IsFinalized;
        entity.FinalizedAt = domain.FinalizedAt;
        entity.NextFollowUpDate = domain.NextFollowUpDate;

        // Update medicines: remove existing and re-add from domain
        var existingMeds = entity.PrescriptionMedicinePrescriptions.ToList();
        if (existingMeds.Any())
        {
            _context.PrescriptionMedicines.RemoveRange(existingMeds);
        }

        foreach (var med in domain.Medicines)
        {
            Infrastructure.Persistence.Entities.Medicine? medicineEntity = null;

            if (med.MedicineId > 0)
            {
                medicineEntity = await _context.Medicines.FindAsync(med.MedicineId);
                if (medicineEntity == null)
                    throw new InvalidOperationException($"Medicine with id {med.MedicineId} not found.");
            }
            else
            {
                medicineEntity = await _context.Medicines.FirstOrDefaultAsync(m => m.Name == med.MedicineName);
                if (medicineEntity == null)
                {
                    medicineEntity = new Infrastructure.Persistence.Entities.Medicine
                    {
                        MedicineGuid = Guid.NewGuid(),
                        Name = med.MedicineName
                    };

                    await _context.Medicines.AddAsync(medicineEntity);
                    await _context.SaveChangesAsync();
                }
            }

            _context.PrescriptionMedicines.Add(new Infrastructure.Persistence.Entities.PrescriptionMedicine
            {
                PrescriptionId = entity.PrescriptionId,
                PrescriptionGuid = entity.PrescriptionGuid,
                MedicineId = medicineEntity.MedicineId,
                MedicineName = medicineEntity.Name,
                Dosage = med.Dosage,
                Frequency = med.Frequency,
                Instructions = med.Instructions,
                Duration = med.DurationDays
            });
        }

        // Update therapies: remove existing and re-add from domain
        var existingTherapies = entity.PrescriptionTherapyPrescriptions.ToList();
        if (existingTherapies.Any())
        {
            _context.PrescriptionTherapies.RemoveRange(existingTherapies);
        }

        foreach (var therapy in domain.Therapies)
        {
            var therapyEntity = await _context.Therapies.FirstOrDefaultAsync(t => t.TherapyId == therapy.TherapyId);
            if (therapyEntity == null)
                throw new InvalidOperationException($"Therapy with id {therapy.TherapyId} not found.");

            _context.PrescriptionTherapies.Add(new Infrastructure.Persistence.Entities.PrescriptionTherapy
            {
                PrescriptionId = entity.PrescriptionId,
                PrescriptionGuid = entity.PrescriptionGuid,
                TherapyId = therapy.TherapyId,
                Notes = therapy.Notes,
                Sessions = therapy.Sessions
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task AutoFinalizeExpiredAsync()
    {
        var now = DateTime.UtcNow;

        var expired = await _context.Prescriptions
            .Where(p => !p.IsFinalized &&
                        p.NextFollowUpDate != null &&
                        p.NextFollowUpDate < now)
            .ToListAsync();

        foreach (var p in expired)
        {
            p.IsFinalized = true;
            p.FinalizedAt = now;
        }

        await _context.SaveChangesAsync();
    }

    private Prescription MapToDomain(Infrastructure.Persistence.Entities.Prescription entity)
    {
        var domain = new Prescription(
            entity.PrescriptionGuid,
            entity.VisitGuid,
            entity.Notes,
            entity.NextFollowUpDate,
            entity.CreatedDate,
            entity.IsFinalized,
            entity.FinalizedAt);

        foreach (var med in entity.PrescriptionMedicinePrescriptions)
        {
            // When mapping from persistence we must not run the editability checks which are intended
            // to prevent modifications on finalized/expired prescriptions. Allow adding medicines and
            // therapies to the domain object without those checks so GET operations don't throw.
            domain.AddMedicine(new PrescriptionMedicine(
                domain.PrescriptionId,
                med.MedicineId, 
                med.MedicineName,
                med.Dosage,
                med.Frequency,
                med.Instructions,
                med.Duration),
                skipCheck: true);
        }

        foreach (var therapy in entity.PrescriptionTherapyPrescriptions)
        {
            domain.AddTherapy(new PrescriptionTherapy(
                domain.PrescriptionId,
                therapy.TherapyId,
                therapy.Sessions.GetValueOrDefault(),
                therapy.Notes),
                skipCheck: true);
        }

        if (entity.IsFinalized)
            domain.Finalize();

        return domain;
    }
}