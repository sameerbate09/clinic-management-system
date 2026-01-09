using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs
{
    public class TherapyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateTherapyRequest
    {
        public string Name { get; set; }
    }

    public class UpdateTherapyRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
