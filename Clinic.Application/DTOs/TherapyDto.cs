namespace Clinic.Application.DTOs;

public class TherapyResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CreateTherapyRequest
{
    public string Name { get; set; }
}

public class UpdateTherapyRequest
{
    public string Name { get; set; } = null!;
}