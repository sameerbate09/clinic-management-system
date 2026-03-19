using System;
using System.Collections.Generic;

namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Address
{
    public Guid AddressId { get; set; }

    public Guid PatientGuid { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? Pincode { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
