using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly ClinicDbContext _context;

    public AdminRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> GetByUsernameAsync(string username)
    {
        var entity = await _context.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Username == username);

        if (entity == null) return null;

        return new Admin(
            entity.AdminGuid,
            entity.Username,
            entity.PasswordHash);
    }
}
