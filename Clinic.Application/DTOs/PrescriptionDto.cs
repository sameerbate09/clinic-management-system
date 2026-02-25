namespace Clinic.Application.DTOs
{
    public class CreatePrescriptionRequest
    {
        public Guid VisitId { get; set; }
        public string Notes { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public List<PrescriptionMedicineDto> Medicines { get; set; }
        public List<PrescriptionTherapyDto> Therapies { get; set; }
    }
    public class UpdatePrescriptionRequest
    {
        public string Notes { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public List<PrescriptionMedicineDto> Medicines { get; set; }
        public List<PrescriptionTherapyDto> Therapies { get; set; }
    }
    public class PrescriptionResponseDto
    {
        public Guid PrescriptionId { get; set; }
        public Guid VisitId { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public bool IsFinalized { get; set; }
        public DateTime? FinalizedAt { get; set; }

        public List<PrescriptionMedicineDto> Medicines { get; set; } = new();
        public List<PrescriptionTherapyDto> Therapies { get; set; } = new();
    }
}
