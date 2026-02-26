using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories;

public interface ITherapyRepository
{
    Task<List<Therapy>> GetAllAsync();

    Task AddAsync(Therapy therapy);
    Task UpdateAsync(int id, string name);
    Task<bool> DeleteAsync(int therapyId);
    Task<bool> ReactivateAsync(int therapyId);
}