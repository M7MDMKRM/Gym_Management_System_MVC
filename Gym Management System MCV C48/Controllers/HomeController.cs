using AutoMapper;
using Gym_Management_System_MCV_C48.Contexts;
using Gym_Management_System_MCV_C48.ViewModels.HomeViewModels;
using Gym_Management_System_MCV_C48.ViewModels.MembershipViewModels;
using Gym_Management_System_MCV_C48.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System_MCV_C48.Controllers
{
    public class HomeController : Controller
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(GymDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;

            var totalMembers = await _context.Members.CountAsync();
            var activeMembers = await _context.Memberships
                .Where(m => m.EndDate > now)
                .Select(m => m.MemberId)
                .Distinct()
                .CountAsync();

            var totalTrainers = await _context.Trainers.CountAsync();
            var totalPlans = await _context.Plans.CountAsync();
            var activeSessions = await _context.Sessions.CountAsync(s => s.EndDate >= now);

            var totalRevenue = await _context.Memberships
                .Include(m => m.Plan)
                .SumAsync(m => m.Plan != null ? m.Plan.Price : 0);

            var recentMemberships = await _context.Memberships
                .Include(m => m.Member)
                .Include(m => m.Plan)
                .OrderByDescending(m => m.CreatedAt)
                .Take(5)
                .ToListAsync();

            var upcomingSessions = await _context.Sessions
                .Include(s => s.Trainer)
                .Where(s => s.EndDate >= now)
                .OrderBy(s => s.StartDate)
                .Take(4)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                TotalMembers = totalMembers,
                ActiveMembers = activeMembers,
                TotalTrainers = totalTrainers,
                TotalPlans = totalPlans,
                ActiveSessions = activeSessions,
                TotalRevenue = totalRevenue,
                RecentMemberships = _mapper.Map<List<MembershipListViewModel>>(recentMemberships),
                UpcomingSessions = _mapper.Map<List<SessionListViewModel>>(upcomingSessions)
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
