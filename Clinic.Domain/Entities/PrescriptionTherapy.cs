using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

public class PrescriptionTherapy
{
    public int Id { get; private set; }              // PK
    public Guid PrescriptionId { get; private set; }  // FK
    public int TherapyId { get; private set; }       // Master therapy
    public int Sessions { get; private set; }        // Number of sessions
    public string? Notes { get; private set; }       // Optional notes for therapy

    internal PrescriptionTherapy(Guid prescriptionId, int therapyId, int sessions, string? notes = null)
    {
        PrescriptionId = prescriptionId;
        TherapyId = therapyId;
        Sessions = sessions;
        Notes = notes;
    }
}

