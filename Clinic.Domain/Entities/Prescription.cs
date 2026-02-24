namespace Clinic.Domain.Entities;

public class Prescription
{
    public Guid PrescriptionId { get; private set; }
    public Guid VisitId { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? NextFollowUpDate { get; private set; }
    public bool IsFinalized { get; private set; }
    public DateTime? FinalizedAt { get; private set; }

    private readonly List<PrescriptionMedicine> _medicines = new();
    private readonly List<PrescriptionTherapy> _therapies = new();

    public IReadOnlyCollection<PrescriptionMedicine> Medicines => _medicines;
    public IReadOnlyCollection<PrescriptionTherapy> Therapies => _therapies;

    private Prescription() { } // EF

    public Prescription(Guid visitId, string? notes, DateTime? nextFollowUpDate)
    {
        PrescriptionId = Guid.NewGuid();
        VisitId = visitId;
        Notes = notes;
        NextFollowUpDate = nextFollowUpDate;
        CreatedDate = DateTime.UtcNow;
        IsFinalized = false;
    }

    // Constructor used when mapping from persistence so the original PrescriptionGuid and dates are preserved
    public Prescription(Guid prescriptionId, Guid visitId, string? notes, DateTime? nextFollowUpDate, DateTime createdDate, bool isFinalized, DateTime? finalizedAt)
    {
        PrescriptionId = prescriptionId;
        VisitId = visitId;
        Notes = notes;
        NextFollowUpDate = nextFollowUpDate;
        CreatedDate = createdDate;
        IsFinalized = isFinalized;
        FinalizedAt = finalizedAt;
    }

    public void AddMedicine(PrescriptionMedicine medicine, bool skipCheck = false)
    {
        if (!skipCheck) EnsureEditable();
        _medicines.Add(medicine);
    }

    public void AddTherapy(PrescriptionTherapy therapy, bool skipCheck = false)
    {
        if (!skipCheck) EnsureEditable();
        _therapies.Add(therapy);
    }

    public void UpdateNotes(string? notes)
    {
        EnsureEditable();
        Notes = notes;
    }

    public void Finalize()
    {
        if (IsFinalized) return;

        IsFinalized = true;
        FinalizedAt = DateTime.UtcNow;
    }

    public bool ShouldAutoFinalize(DateTime now)
    {
        return !IsFinalized
            && NextFollowUpDate.HasValue
            && NextFollowUpDate.Value < now;
    }

    private void EnsureEditable()
    {
        if (IsFinalized ||
            (NextFollowUpDate.HasValue &&
             NextFollowUpDate.Value < DateTime.UtcNow))
        {
            throw new InvalidOperationException(
                "Prescription is finalized or expired.");
        }
    }
}
