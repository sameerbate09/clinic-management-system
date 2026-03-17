using Clinic.Application.DTOs;

namespace Clinic.Application.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardSummaryAsync();
    }
}
