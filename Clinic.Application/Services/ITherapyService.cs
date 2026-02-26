using Clinic.Application.DTOs;

namespace Clinic.Application.Services;

public interface ITherapyService
{
    Task<List<TherapyResponse>> GetAllTherapiesAsync();
    Task<TherapyResponse> AddTherapyAsync(CreateTherapyRequest request);
    Task UpdateTherapyAsync(int id, string name);
    Task DeleteTherapyAsync(int therapyId);
    Task ReactivateTherapyAsync(int therapyId);
}