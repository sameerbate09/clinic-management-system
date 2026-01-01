using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs
{
    public class CreatePrescriptionRequest
    {
        public int VisitId { get; set; }
        public string Advice { get; set; }
        public List<PrescriptionMedicineDto> Medicines { get; set; }
        public List<PrescriptionTherapyDto> Therapies { get; set; }
    }

}
