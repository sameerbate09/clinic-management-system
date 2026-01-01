using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs
{
    public class CreatePatientRequest
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Concern { get; set; }
    }

    public class PatientResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }


}
