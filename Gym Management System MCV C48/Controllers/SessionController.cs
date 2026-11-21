using AutoMapper;
using Gym_Management_System_MCV_C48.Contexts;
using Gym_Management_System_MCV_C48.Entities;
using Gym_Management_System_MCV_C48.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System_MCV_C48.Controllers
{
    public class SessionController : Controller
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;

        public SessionController(GymDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: /Session/Index
        public async Task<IActionResult> Index()
        {
            var sessions = await _context.Sessions
                .Include(s => s.Trainer)
                .OrderBy(s => s.StartDate)
                .ToListAsync();

            var viewModels = _mapper.Map<List<SessionListViewModel>>(sessions);
            return View(viewModels);
        }

        // GET: /Session/Create
        public async Task<IActionResult> Create()
        {
            var model = new SessionCreateViewModel
            {
                TrainersList = await GetTrainersSelectListAsync()
            };
            return View(model);
        }

        // POST: /Session/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionCreateViewModel model)
        {
            if (model.EndDate <= model.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date and time must be after the start date and time.");
            }

            if (!ModelState.IsValid)
            {
                model.TrainersList = await GetTrainersSelectListAsync();
                return View(model);
            }

            var session = _mapper.Map<Session>(model);
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Session scheduled successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Session/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<SessionEditViewModel>(session);
            model.TrainersList = await GetTrainersSelectListAsync();
            return View(model);
        }

        // POST: /Session/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SessionEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (model.EndDate <= model.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date and time must be after the start date and time.");
            }

            if (!ModelState.IsValid)
            {
                model.TrainersList = await GetTrainersSelectListAsync();
                return View(model);
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            session.Description = model.Description;
            session.Capacity = model.Capacity;
            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;
            session.TrainerId = model.TrainerId;
            session.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Session updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Session/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Session deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainersSelectListAsync()
        {
            return await _context.Trainers
                .OrderBy(t => t.Name)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.Name} ({t.Specialties})"
                })
                .ToListAsync();
        }
    }
}

