using System;
using System.Collections.Generic;

namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public Guid MedicineGuid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
}
