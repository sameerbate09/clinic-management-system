using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs
{
    public class PrescriptionMedicineDto
    {
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public int DurationDays { get; set; }
    }

}
