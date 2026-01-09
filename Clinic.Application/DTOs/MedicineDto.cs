using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs
{
    public class CreateMedicineRequest
    {
        public string Name { set; get; }
        public string? Description { set; get; }
    }

    public class MedicineResponse
    {
        public Guid MedicineGuid { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class MedicineUpdateRequest
    {
        public Guid MedicineGuid { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
