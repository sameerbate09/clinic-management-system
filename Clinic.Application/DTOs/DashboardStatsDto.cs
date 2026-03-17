namespace Clinic.Application.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalPatients { get; set; }
        public int TodaysVisits { get; set; }
        public int TodaysFollowUpCount { get; set; }
        public List<FollowUpDetailsDto> TodaysFollowUps { get; set; } = new();
        public List<RecentVisitDto> RecentVisits { get; set; } = new();
    }
    public class FollowUpDetailsDto
    {
        public string PatientName { get; set; }
        public DateTime? FollowUpDate { get; set; } 
        public string Concern { get; set; }
    }

    public class RecentVisitDto
    {
        public string PatientName { get; set; }
        public DateTime VisitDate { get; set; }
        public string Complaint { get; set; }
    }
}
