using Gym_Management_System_MCV_C48.ViewModels.MembershipViewModels;
using Gym_Management_System_MCV_C48.ViewModels.SessionViewModels;

namespace Gym_Management_System_MCV_C48.ViewModels.HomeViewModels
{
    public class DashboardViewModel
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int TotalPlans { get; set; }
        public int ActiveSessions { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<MembershipListViewModel> RecentMemberships { get; set; } = new();
        public List<SessionListViewModel> UpcomingSessions { get; set; } = new();
    }
}

