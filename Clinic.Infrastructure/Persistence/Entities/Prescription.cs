using System;
using System.Collections.Generic;

namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int VisitId { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid VisitGuid { get; set; }

    public Guid PrescriptionGuid { get; set; }

    public DateTime? NextFollowUpDate { get; set; }

    public bool IsFinalized { get; set; }

    public DateTime? FinalizedAt { get; set; }

    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicinePrescriptionNavigations { get; set; } = new List<PrescriptionMedicine>();

    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicinePrescriptions { get; set; } = new List<PrescriptionMedicine>();

    public virtual ICollection<PrescriptionTherapy> PrescriptionTherapyPrescriptionNavigations { get; set; } = new List<PrescriptionTherapy>();

    public virtual ICollection<PrescriptionTherapy> PrescriptionTherapyPrescriptions { get; set; } = new List<PrescriptionTherapy>();

    public virtual Visit Visit { get; set; } = null!;

    public virtual Visit VisitNavigation { get; set; } = null!;
}
