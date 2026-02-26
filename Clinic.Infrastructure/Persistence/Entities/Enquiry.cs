namespace Clinic.Infrastructure.Persistence.Entities;

public partial class Enquiry
{
    public int EnquiryId { get; set; }

    public string Name { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string? Email { get; set; }

    public string Message { get; set; } = null!;

    public bool IsContacted { get; set; }

    public DateTime CreatedDate { get; set; }
}