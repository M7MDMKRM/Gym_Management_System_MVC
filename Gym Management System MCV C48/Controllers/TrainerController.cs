using AutoMapper;
using Gym_Management_System_MCV_C48.Contexts;
using Gym_Management_System_MCV_C48.Entities;
using Gym_Management_System_MCV_C48.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System_MCV_C48.Controllers
{
    public class TrainerController : Controller
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;

        public TrainerController(GymDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: /Trainer/Index
        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers
                .Include(t => t.Sessions)
                .OrderBy(t => t.Name)
                .ToListAsync();

            var viewModels = _mapper.Map<List<TrainerListViewModel>>(trainers);
            return View(viewModels);
        }

        // GET: /Trainer/Create
        public IActionResult Create()
        {
            return View(new TrainerCreateViewModel());
        }

        // POST: /Trainer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainerCreateViewModel model)
        {
            if (await _context.Trainers.AnyAsync(t => t.Email == model.Email))
            {
                ModelState.AddModelError("Email", "A trainer with this email already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var trainer = _mapper.Map<Trainer>(model);
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Trainer '{trainer.Name}' was added successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Trainer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<TrainerEditViewModel>(trainer);
            return View(model);
        }

        // POST: /Trainer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainerEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (await _context.Trainers.AnyAsync(t => t.Email == model.Email && t.Id != id))
            {
                ModelState.AddModelError("Email", "Another trainer is using this email address.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            trainer.Name = model.Name;
            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.DateOfBirth = model.DateOfBirth;
            trainer.Gender = model.Gender;
            trainer.HireDate = model.HireDate;
            trainer.Specialties = model.Specialties;
            trainer.Address.BuildingNumber = model.BuildingNumber;
            trainer.Address.Street = model.Street;
            trainer.Address.City = model.City;
            trainer.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Trainer '{trainer.Name}' was updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Trainer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var trainer = await _context.Trainers
                .Include(t => t.Sessions)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trainer == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<TrainerDetailsViewModel>(trainer);
            return View(viewModel);
        }

        // POST: /Trainer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Trainer '{trainer.Name}' and their sessions were deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}

