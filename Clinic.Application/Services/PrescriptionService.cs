using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;

namespace Clinic.Application.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _repository;

        public PrescriptionService(IPrescriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PrescriptionResponseDto?> GetByVisitGuidAsync(Guid visitGuid)
        {
            var prescription = await _repository.GetByVisitGuidAsync(visitGuid);
            if (prescription == null) return null;

            return new PrescriptionResponseDto
            {
                PrescriptionId = prescription.PrescriptionId,
                VisitId = prescription.VisitId,
                Notes = prescription.Notes,
                CreatedDate = prescription.CreatedDate,
                NextFollowUpDate = prescription.NextFollowUpDate,
                IsFinalized = prescription.IsFinalized,
                FinalizedAt = prescription.FinalizedAt,
                Medicines = prescription.Medicines
                    .Select(m => new PrescriptionMedicineDto
                    {
                        MedicineId = m.MedicineId,
                        MedicineName = m.MedicineName,
                        Dosage = m.Dosage,
                        Frequency = m.Frequency,
                        Instructions = m.Instructions,
                        DurationDays = m.DurationDays
                    }).ToList(),
                Therapies = prescription.Therapies
                    .Select(t => new PrescriptionTherapyDto
                    {
                        TherapyId = t.TherapyId,
                        Sessions = t.Sessions
                    }).ToList()
            };
        }

        public async Task<Guid> CreateAsync(CreatePrescriptionRequest request)
        {
            var prescription = new Prescription(
                request.VisitId,
                request.Notes,
                request.NextFollowUpDate);

            foreach (var med in request.Medicines)
            {
                prescription.AddMedicine(
                    new PrescriptionMedicine(
                        prescription.PrescriptionId,
                        med.MedicineId,
                        med.MedicineName,
                        med.Dosage,
                        med.Frequency,
                        med.Instructions,
                        med.DurationDays));
            }

            foreach (var therapy in request.Therapies)
            {
                prescription.AddTherapy(
                    new PrescriptionTherapy(
                        prescription.PrescriptionId,
                        therapy.TherapyId,
                        therapy.Notes));
            }

            return await _repository.CreateAsync(prescription);
        }

        public async Task<PrescriptionResponseDto?> GetAsync(Guid prescriptionGuid)
        {
            var prescription = await _repository.GetByGuidAsync(prescriptionGuid);
            if (prescription == null) return null;

            return new PrescriptionResponseDto
            {
                PrescriptionId = prescription.PrescriptionId,
                VisitId = prescription.VisitId,
                Notes = prescription.Notes,
                CreatedDate = prescription.CreatedDate,
                NextFollowUpDate = prescription.NextFollowUpDate,
                IsFinalized = prescription.IsFinalized,
                FinalizedAt = prescription.FinalizedAt,
                Medicines = prescription.Medicines
                    .Select(m => new PrescriptionMedicineDto
                    {
                        MedicineName = m.MedicineName,
                        Dosage = m.Dosage,
                        Frequency = m.Frequency,
                        Instructions = m.Instructions,
                        DurationDays = m.DurationDays
                    }).ToList(),
                Therapies = prescription.Therapies
                    .Select(t => new PrescriptionTherapyDto
                    {
                        TherapyId = t.TherapyId,
                        Sessions = t.Sessions
                    }).ToList()
            };
        }

        public async Task UpdateNotesAsync(Guid prescriptionGuid, string? notes)
        {
            var prescription = await _repository.GetByGuidAsync(prescriptionGuid);
            if (prescription == null) return;

            prescription.UpdateNotes(notes);
            await _repository.UpdateAsync(prescription);
        }

        public Task AutoFinalizeExpiredAsync()
            => _repository.AutoFinalizeExpiredAsync();
    }
}
