using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public partial class Address
{
    public Guid AddressId { get; set; }

    public Guid PatientGuid { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? Pincode { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public Address(string? street, string? city, string? pincode)
    {
        Street = street;
        City = city;
        Pincode = pincode;
    }
}


