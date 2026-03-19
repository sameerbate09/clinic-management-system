using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs;

public class AddressDto
{
    public string Street { get; set; }
    public string City { get; set; }
    public string Pincode { get; set; }
}
