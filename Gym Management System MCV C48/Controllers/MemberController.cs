using AutoMapper;
using Gym_Management_System_MCV_C48.Contexts;
using Gym_Management_System_MCV_C48.Entities;
using Gym_Management_System_MCV_C48.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System_MCV_C48.Controllers
{
    public class MemberController : Controller
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public MemberController(GymDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        // GET: /Member/Index
        public async Task<IActionResult> Index(string? search)
        {
            var query = _context.Members
                .Include(m => m.Memberships)
                .ThenInclude(m => m.Plan)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(m => m.Name.ToLower().Contains(search) ||
                                         m.Email.ToLower().Contains(search) ||
                                         m.Phone.Contains(search));
            }

            var members = await query.OrderByDescending(m => m.CreatedAt).ToListAsync();
            var viewModels = _mapper.Map<List<MemberListViewModel>>(members);

            ViewBag.Search = search;
            return View(viewModels);
        }

        // GET: /Member/Create
        public IActionResult Create()
        {
            return View(new MemberCreateViewModel());
        }

        // POST: /Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberCreateViewModel model)
        {
            if (await _context.Members.AnyAsync(m => m.Email == model.Email))
            {
                ModelState.AddModelError("Email", "A member with this email already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var member = _mapper.Map<Member>(model);

            // Handle photo upload
            if (model.PhotoFile != null && model.PhotoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "members");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.PhotoFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.PhotoFile.CopyToAsync(stream);
                }

                member.Photo = $"/uploads/members/{uniqueFileName}";
            }

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Member '{member.Name}' added successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Member/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<MemberEditViewModel>(member);
            return View(model);
        }

        // POST: /Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MemberEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (await _context.Members.AnyAsync(m => m.Email == model.Email && m.Id != id))
            {
                ModelState.AddModelError("Email", "Another member is using this email address.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            member.Name = model.Name;
            member.Email = model.Email;
            member.Phone = model.Phone;
            member.DateOfBirth = model.DateOfBirth;
            member.Gender = model.Gender;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.Street = model.Street;
            member.Address.City = model.City;
            member.UpdatedAt = DateTime.Now;

            // Handle photo upload if new one provided
            if (model.NewPhotoFile != null && model.NewPhotoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "members");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.NewPhotoFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.NewPhotoFile.CopyToAsync(stream);
                }

                member.Photo = $"/uploads/members/{uniqueFileName}";
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Member '{member.Name}' updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Member/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var member = await _context.Members
                .Include(m => m.Memberships)
                .ThenInclude(m => m.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<MemberDetailsViewModel>(member);
            return View(viewModel);
        }

        // POST: /Member/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Member '{member.Name}' and their memberships were deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}

