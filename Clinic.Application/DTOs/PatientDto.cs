namespace Clinic.Application.DTOs;

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
    public Guid PatientId { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
}