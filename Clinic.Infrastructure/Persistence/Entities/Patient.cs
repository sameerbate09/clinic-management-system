using System;
using System.Collections.Generic;

namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Patient
{
    public string Name { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Concern { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid PatientGuid { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
