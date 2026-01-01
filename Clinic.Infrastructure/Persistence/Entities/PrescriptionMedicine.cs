using System;
using System.Collections.Generic;

namespace Clinic.Infrastructure.Persistence.Entities;

public partial class PrescriptionMedicine
{
    public int PrescriptionMedicineId { get; set; }

    public int PrescriptionId { get; set; }

    public string MedicineName { get; set; } = null!;

    public string? Dosage { get; set; }

    public string? Frequency { get; set; }

    public string? Duration { get; set; }

    public string? Instructions { get; set; }

    public virtual Prescription Prescription { get; set; } = null!;
}
