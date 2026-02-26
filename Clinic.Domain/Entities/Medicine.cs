namespace Clinic.Domain.Entities;

public class Medicine
{
    public Guid MedicineGuid { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public Medicine(Guid id, string name, string desc)
    {
        MedicineGuid = id;
        Name = name;
        Description = desc;

    }

    public Medicine(string name, string desc)
    {
        Name = name;
        Description = desc;
    }

    public Medicine(Guid id)
    {
        MedicineGuid = id;
    }
}