namespace Clinic.Application.DTOs;

public class CreateVisitDto
{
    public Guid PatientId { get; set; }
    public string Complaint { get; set; }
    public string Notes { get; set; }
}

public class VisitResponseDto
{
    public Guid VisitId { get; set; }
    public DateTime VisitDate { get; set; }
    public string Complaint { get; set; }
    public string Notes { get; set; }
}

public class VisitSummaryDto
{
    public Guid VisitId { get; set; }
    public DateTime VisitDate { get; set; }
    public string Complaint { get; set; }
}