namespace Clinic.Application.DTOs;

public class CreateMedicineRequest
{
    public string Name { set; get; }
    public string? Description { set; get; }
}

public class MedicineResponse
{
    public Guid MedicineGuid { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class MedicineUpdateRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class MedicineSearchDto
{
    public int MedicineId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
}