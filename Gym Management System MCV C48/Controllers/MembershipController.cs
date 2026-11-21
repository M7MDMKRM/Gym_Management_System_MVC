using AutoMapper;
using Gym_Management_System_MCV_C48.Contexts;
using Gym_Management_System_MCV_C48.Entities;
using Gym_Management_System_MCV_C48.ViewModels.MembershipViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System_MCV_C48.Controllers
{
    public class MembershipController : Controller
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;

        public MembershipController(GymDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: /Membership/Index
        public async Task<IActionResult> Index()
        {
            var memberships = await _context.Memberships
                .Include(m => m.Member)
                .Include(m => m.Plan)
                .OrderByDescending(m => m.StartDate)
                .ToListAsync();

            var viewModels = _mapper.Map<List<MembershipListViewModel>>(memberships);
            return View(viewModels);
        }

        // GET: /Membership/Create
        public async Task<IActionResult> Create(int? memberId)
        {
            var model = new MembershipCreateViewModel
            {
                MemberId = memberId ?? 0,
                StartDate = DateTime.Today,
                MembersList = await GetMembersSelectListAsync(),
                PlansList = await GetActivePlansSelectListAsync()
            };

            return View(model);
        }

        // POST: /Membership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembershipCreateViewModel model)
        {
            var plan = await _context.Plans.FindAsync(model.PlanId);
            if (plan == null || !plan.IsActive)
            {
                ModelState.AddModelError("PlanId", "Selected plan is inactive or not found.");
            }

            var member = await _context.Members.FindAsync(model.MemberId);
            if (member == null)
            {
                ModelState.AddModelError("MemberId", "Selected member not found.");
            }

            if (!ModelState.IsValid || plan == null)
            {
                model.MembersList = await GetMembersSelectListAsync();
                model.PlansList = await GetActivePlansSelectListAsync();
                return View(model);
            }

            var membership = new Membership
            {
                MemberId = model.MemberId,
                PlanId = model.PlanId,
                StartDate = model.StartDate,
                EndDate = model.StartDate.AddDays(plan.DurationDays)
            };

            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Plan '{plan.Name}' assigned to '{member!.Name}' successfully until {membership.EndDate:dd/MM/yyyy}.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Membership/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var membership = await _context.Memberships
                .Include(m => m.Member)
                .Include(m => m.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null)
            {
                return NotFound();
            }

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Membership was cancelled successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetMembersSelectListAsync()
        {
            return await _context.Members
                .OrderBy(m => m.Name)
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = $"{m.Name} ({m.Phone})"
                })
                .ToListAsync();
        }

        private async Task<IEnumerable<SelectListItem>> GetActivePlansSelectListAsync()
        {
            return await _context.Plans
                .Where(p => p.IsActive)
                .OrderBy(p => p.Price)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Name} — {p.Price:N0} EGP ({p.DurationDays} days)"
                })
                .ToListAsync();
        }
    }
}

