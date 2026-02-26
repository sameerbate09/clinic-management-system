namespace Clinic.Infrastructure.Persistence.Entities;

public partial class PrescriptionMedicine
{
    public int PrescriptionMedicineId { get; set; }

    public int PrescriptionId { get; set; }

    public string? Dosage { get; set; }

    public string? Frequency { get; set; }

    public string? Duration { get; set; }

    public string? Instructions { get; set; }

    public Guid PrescriptionGuid { get; set; }

    public int MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;

    public virtual Prescription PrescriptionNavigation { get; set; } = null!;
}