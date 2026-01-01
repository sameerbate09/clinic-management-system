using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs
{
    public class PrescriptionTherapyDto
    {
        public int TherapyId { get; set; }
        public int Sessions { get; set; }
        public decimal? Cost { get; set; }
    }

}
