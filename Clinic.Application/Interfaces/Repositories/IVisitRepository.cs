using Clinic.Application.DTOs;
using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories;

public interface IVisitRepository
{
    Task<IEnumerable<Visit>> GetByPatientIdAsync(Guid patientId);
    Task<Visit> GetByIdAsync(Guid visitId);
    Task<Guid> AddAsync(Visit visit);
    Task<int> GetVisitsCountByDateAsync(DateTime date);
    Task<List<RecentVisitDto>> GetRecentVisitsAsync(int count);
}