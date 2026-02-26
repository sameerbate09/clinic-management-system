namespace Clinic.Domain.Entities;

public class Admin
{
    public Guid AdminId { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }

    private Admin() { }

    public Admin(Guid adminId, string username, string passwordHash)
    {
        AdminId = adminId;
        Username = username;
        PasswordHash = passwordHash;
    }
}
