namespace Clinic.Domain.Entities;

public class PrescriptionTherapy
{
    public int Id { get; private set; }              // PK
    public Guid PrescriptionId { get; private set; }  // FK
    public int TherapyId { get; private set; }       // Master therapy
    public int Sessions { get; private set; }        // Number of sessions
    public string? Notes { get; private set; }       // Optional notes for therapy

    private PrescriptionTherapy() { }

    public PrescriptionTherapy(Guid prescriptionId, int therapyId, string? notes = null)
    {
        PrescriptionId = prescriptionId;
        TherapyId = therapyId;
        Notes = notes;
    }
}

