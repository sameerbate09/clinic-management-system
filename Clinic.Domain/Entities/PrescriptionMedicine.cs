namespace Clinic.Domain.Entities;

public class PrescriptionMedicine
{
    public int Id { get; private set; }
    public Guid PrescriptionId { get; private set; }
    public int MedicineId { get; private set; }
    public string? MedicineName { get; private set; }
    public string Dosage { get; private set; }
    public string Frequency { get; private set; }
    public string Instructions { get; private set; }
    public string DurationDays { get; private set; }

    private PrescriptionMedicine() { }

    public PrescriptionMedicine(Guid prescriptionId, int medicineId, string medicineName, string dosage, string frequency, string instructions, string durationDays)
    {
        PrescriptionId = prescriptionId;
        MedicineId = medicineId;
        MedicineName = medicineName;
        Dosage = dosage;
        Frequency = frequency;
        Instructions = instructions;
        DurationDays = durationDays;
    }
}
