using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs
{
    public class CreateVisitRequest
    {
        public int PatientId { get; set; }
        public string Notes { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
    }

}
