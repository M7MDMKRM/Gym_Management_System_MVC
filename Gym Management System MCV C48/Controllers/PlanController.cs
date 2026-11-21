using AutoMapper;
using Gym_Management_System_MCV_C48.Contexts;
using Gym_Management_System_MCV_C48.Entities;
using Gym_Management_System_MCV_C48.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System_MCV_C48.Controllers
{
    public class PlanController : Controller
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;

        public PlanController(GymDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: /Plan/Index
        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;

            var plans = await _context.Plans
                .Include(p => p.Memberships)
                .OrderBy(p => p.Price)
                .ToListAsync();

            var viewModels = plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive,
                ActiveMembershipsCount = p.Memberships.Count(m => m.EndDate > now)
            }).ToList();

            return View(viewModels);
        }

        // GET: /Plan/Create
        public IActionResult Create()
        {
            return View(new PlanCreateViewModel());
        }

        // POST: /Plan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var plan = _mapper.Map<Plan>(model);
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Plan '{plan.Name}' was created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Plan/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _context.Plans
                .Include(p => p.Memberships)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plan == null)
            {
                return NotFound();
            }

            var now = DateTime.Now;
            var activeMemberships = plan.Memberships.Count(m => m.EndDate > now);

            var model = _mapper.Map<PlanEditViewModel>(plan);
            model.IsLocked = activeMemberships > 0;

            if (model.IsLocked)
            {
                ViewBag.LockWarning = "This plan has active memberships and cannot be deactivated or have its duration changed.";
            }

            return View(model);
        }

        // POST: /Plan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var plan = await _context.Plans
                .Include(p => p.Memberships)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plan == null)
            {
                return NotFound();
            }

            var now = DateTime.Now;
            var isLocked = plan.Memberships.Any(m => m.EndDate > now);

            if (!ModelState.IsValid)
            {
                model.IsLocked = isLocked;
                return View(model);
            }

            if (isLocked)
            {
                // Can only update description and price, duration and status are locked
                plan.Name = model.Name;
                plan.Description = model.Description;
                plan.Price = model.Price;
                plan.UpdatedAt = DateTime.Now;
            }
            else
            {
                plan.Name = model.Name;
                plan.Description = model.Description;
                plan.DurationDays = model.DurationDays;
                plan.Price = model.Price;
                plan.IsActive = model.IsActive;
                plan.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Plan '{plan.Name}' was updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Plan/ToggleStatus/5 (SoftDelete / Deactivate / Activate)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var plan = await _context.Plans
                .Include(p => p.Memberships)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plan == null)
            {
                return NotFound();
            }

            var now = DateTime.Now;
            var hasActiveMemberships = plan.Memberships.Any(m => m.EndDate > now);

            if (plan.IsActive && hasActiveMemberships)
            {
                TempData["ErrorMessage"] = $"Plan '{plan.Name}' cannot be deactivated because it has active memberships.";
                return RedirectToAction(nameof(Index));
            }

            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            var statusStr = plan.IsActive ? "activated" : "deactivated";
            TempData["SuccessMessage"] = $"Plan '{plan.Name}' has been {statusStr}.";
            return RedirectToAction(nameof(Index));
        }
    }
}

