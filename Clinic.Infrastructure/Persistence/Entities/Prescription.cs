using System;
using System.Collections.Generic;

namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int VisitId { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();

    public virtual ICollection<PrescriptionTherapy> PrescriptionTherapies { get; set; } = new List<PrescriptionTherapy>();

    public virtual Visit Visit { get; set; } = null!;
}
