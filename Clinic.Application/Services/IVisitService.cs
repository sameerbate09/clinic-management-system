using Clinic.Application.DTOs;

namespace Clinic.Application.Services;

public interface IVisitService
{
    Task<VisitResponseDto> AddAsync(CreateVisitDto dto);
    Task<IEnumerable<VisitSummaryDto>> GetByPatientIdAsync(Guid patientId);
    Task<VisitResponseDto?> GetByIdAsync(Guid visitId);
    Task<List<RecentVisitDto>> GetAllVisitsAsync();
}