namespace Clinic.Domain.Entities;

public class Visit
{
    public Guid VisitId { get; set; }
    public Guid PatientId { get; set; }
    public DateTime VisitDate { get;  set; }
    public string Notes { get;  set; }
    public string Complaint { get; set; }


    public Visit() { }

    public Visit(Guid patientId, DateTime visitDate, string complaint, string notes)
    {
        PatientId = patientId;
        VisitDate = visitDate;
        Notes = notes;
        Complaint = complaint;
    }
}