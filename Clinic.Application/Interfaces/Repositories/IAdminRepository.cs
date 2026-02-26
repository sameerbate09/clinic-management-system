using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories;

public interface IAdminRepository
{
    Task<Admin?> GetByUsernameAsync(string username);
}
