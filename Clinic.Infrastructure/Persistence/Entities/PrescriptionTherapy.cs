namespace Clinic.Infrastructure.Persistence.Entities;

public partial class PrescriptionTherapy
{
    public int PrescriptionTherapyId { get; set; }

    public int PrescriptionId { get; set; }

    public int TherapyId { get; set; }

    public string? Notes { get; set; }

    public Guid PrescriptionGuid { get; set; }

    public int? Sessions { get; set; }

    public virtual Prescription Prescription { get; set; } = null!;

    public virtual Prescription PrescriptionNavigation { get; set; } = null!;

    public virtual Therapy Therapy { get; set; } = null!;
}