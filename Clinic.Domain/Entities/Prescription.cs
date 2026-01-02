using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

public class Prescription
{
    //public int Id { get; private set; }              // Maps to PrescriptionId
    //public int VisitId { get; private set; }         // FK to Visit
    public Guid PrescriptionId { get; set; }
    public Guid VisitId { get; set; }
    public string Notes { get; private set; }       // Maps to Notes column
    public DateTime CreatedDate { get; private set; }

    private readonly List<PrescriptionTherapy> _therapies = new();
    public IReadOnlyCollection<PrescriptionTherapy> Therapies => _therapies.AsReadOnly();

    public Prescription(Guid visitId, string notes)
    {
        VisitId = visitId;
        Notes = notes;
        CreatedDate = DateTime.UtcNow;
    }

    public void AddTherapy(int therapyId, int sessions, string? notes = null)
    {
        var therapy = new PrescriptionTherapy(PrescriptionId, therapyId, sessions, notes);
        _therapies.Add(therapy);
    }
}

