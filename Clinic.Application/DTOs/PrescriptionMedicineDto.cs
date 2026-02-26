namespace Clinic.Application.DTOs;

public class PrescriptionMedicineDto
{
    public int MedicineId { get; set; }
    public string? MedicineName { get; set; }
    public string Dosage { get; set; }
    public string Frequency { get; set; }
    public string Instructions { get; set; }
    public string DurationDays { get; set; }
}