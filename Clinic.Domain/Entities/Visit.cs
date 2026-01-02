using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

public class Visit
{
    //public int Id { get; private set; }
    //public int PatientId { get; private set; }
    public Guid VisitId { get; set; }
    public Guid PatientId { get; set; }
    public DateTime VisitDate { get; private set; }
    public string Notes { get; private set; }
    public DateTime? NextFollowUpDate { get; private set; }

    public Visit(Guid patientId, DateTime visitDate, string notes, DateTime? nextFollowUpDate)
    {
        PatientId = patientId;
        VisitDate = visitDate;
        Notes = notes;
        NextFollowUpDate = nextFollowUpDate;
    }
}

