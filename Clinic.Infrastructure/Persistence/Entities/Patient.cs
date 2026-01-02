using System;
using System.Collections.Generic;

namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Patient
{
    public int PatientId { get; set; }

    public string Name { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Concern { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public Guid PatientGuid { get; set; }

    public virtual ICollection<Visit> VisitPatientNavigations { get; set; } = new List<Visit>();

    public virtual ICollection<Visit> VisitPatients { get; set; } = new List<Visit>();
}
