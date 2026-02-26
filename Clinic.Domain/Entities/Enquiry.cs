namespace Clinic.Domain.Entities;

public class Enquiry
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Mobile { get; private set; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsRead { get; private set; }

    public Enquiry(string name, string mobile, string message)
    {
        Name = name;
        Mobile = mobile;
        Message = message;
        CreatedAt = DateTime.UtcNow;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}