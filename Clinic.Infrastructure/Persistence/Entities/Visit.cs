using System;
using System.Collections.Generic;

namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Visit
{
    public int VisitId { get; set; }

    public DateTime VisitDate { get; set; }

    public string Complaint { get; set; } = null!;

    public string? Notes { get; set; }

    public Guid PatientGuid { get; set; }

    public Guid VisitGuid { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Prescription? PrescriptionVisitNavigation { get; set; }

    public virtual ICollection<Prescription> PrescriptionVisits { get; set; } = new List<Prescription>();
}
