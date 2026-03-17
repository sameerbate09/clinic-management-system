using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using System.Numerics;

namespace Clinic.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IPatientRepository _patientRepo;
    private readonly IVisitRepository _visitRepo;
    private readonly IPrescriptionRepository _prescriptionRepo;

    public DashboardService(IPatientRepository patientrepo, IVisitRepository visitRepo, IPrescriptionRepository prescriptionRepo)
    {
        _patientRepo = patientrepo;
        _visitRepo = visitRepo;
        _prescriptionRepo = prescriptionRepo;
    }

    public async Task<DashboardStatsDto> GetDashboardSummaryAsync()
    {
        var today = DateTime.Today;

        // Await each one individually so they use the DbContext one at a time
        var totalPatients = await _patientRepo.GetTotalCountAsync();
        var visitsToday = await _visitRepo.GetVisitsCountByDateAsync(today);
        var followUps = await _prescriptionRepo.GetTodaysFollowUpsAsync(today);
        var recentVisits = await _visitRepo.GetRecentVisitsAsync(5);

        return new DashboardStatsDto
        {
            TotalPatients = totalPatients,
            TodaysVisits = visitsToday,
            RecentVisits = recentVisits,
            TodaysFollowUpCount = followUps.Count,
            TodaysFollowUps = followUps
        };
    }
}
