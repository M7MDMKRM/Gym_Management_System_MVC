using Microsoft.AspNetCore.Mvc;
using MVC_Gym.Entities;
using MVC_Gym.Repositories.Entites;
using MVC_Gym.Repositories.Interfaces;
using MVC_Gym.ViewModels;

namespace MVC_Gym.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepositorie _planRepo;
        private readonly IMembershipRepositories _membershipRepo;

        public PlanController(IPlanRepositorie planRepo, IMembershipRepositories membershipRepo)
        {
            _planRepo = planRepo;
            _membershipRepo = membershipRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await _planRepo.GetAllAsync(ct);
            var planModels = plans.Select(p => new PlanViewmodel()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive,
            });

            return View(planModels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var plans = await _planRepo.GetByIdAsync(id, ct);
            if (plans is null || !plans.IsActive)
            {
                TempData["ErrorMessage"] = "Plan Cant Be Edited.";
                return RedirectToAction(nameof(Index));
            }

            var hascurrentMemberships = await _membershipRepo.AnyAsync(M => M.PlanId == id && M.EndDate > DateTime.UtcNow, ct);
            if (hascurrentMemberships)
            {
                TempData["ErrorMessage"] = "Plan Cant Be Edited.";
                return RedirectToAction(nameof(Index));
            }

            var viewmodel = new UpdatedViewmodel()
            {
                PlanName = plans.Name,
                Description = plans.Description,
                Price = plans.Price,
                DurationDays = plans.DurationDays,
            };

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, UpdatedViewmodel updatedPlan, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedPlan);
            }

            var plans = await _planRepo.GetByIdAsync(id, ct);
            if (plans is null || !plans.IsActive)
            {
                TempData["ErrorMessage"] = "Plan Cant Be Edited.";
                return RedirectToAction(nameof(Index));
            }

            var hascurrentMemberships = await _membershipRepo.AnyAsync(M => M.PlanId == id && M.EndDate > DateTime.UtcNow, ct);
            if (hascurrentMemberships)
            {
                TempData["ErrorMessage"] = "Plan Cant Be Edited.";
                return RedirectToAction(nameof(Index));
            }

            plans.Description = updatedPlan.Description;
            plans.DurationDays = updatedPlan.DurationDays;
            plans.Price = updatedPlan.Price;
            plans.UpdatedAt = DateTime.UtcNow;

            var result = await _planRepo.UpdateAsync(ct);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Plan updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update the plan.");
                return View(updatedPlan);
            }
        }
    }
}