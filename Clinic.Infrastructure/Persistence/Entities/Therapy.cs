namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Therapy
{
    public int TherapyId { get; set; }

    public string TherapyName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<PrescriptionTherapy> PrescriptionTherapies { get; set; } = new List<PrescriptionTherapy>();
}