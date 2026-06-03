namespace Clinic.Application.DTOs;

public class PrescriptionMedicineDto
{
    public int MedicineId { get; set; }
    // MedicineName removed; consumers should resolve name from Medicine service if needed
    public string Dosage { get; set; }
    public string Frequency { get; set; }
    public string Instructions { get; set; }
    public string DurationDays { get; set; }
}