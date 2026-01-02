using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

public class PrescriptionMedicine
{
    public int Id { get; private set; }
    public Guid PrescriptionId { get; private set; }
    public string MedicineName { get; private set; }
    public string Dosage { get; private set; }
    public int DurationDays { get; private set; }

    public PrescriptionMedicine(Guid prescriptionId, string medicineName, string dosage, int durationDays)
    {
        PrescriptionId = prescriptionId;
        MedicineName = medicineName;
        Dosage = dosage;
        DurationDays = durationDays;
    }
}
