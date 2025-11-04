using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammedPortfolio.Data;
using MohammedPortfolio.Models;
using MohammedPortfolio.Models.ViewModels;

namespace MohammedPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext db)
        {
            _context = db;
        }

        // ====================== INDEX ======================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ServiceViewModel
            {
                Services = await _context.Service_.ToListAsync(),
                ImplementationSteps = await _context.ImplementationStep_.ToListAsync(),
            };
            return View(viewModel);
        }

        // ====================== DETAILS ======================
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = new ServiceViewModel
            {
                Services = await _context.Service_
                    .Where(x => x.Id == id).ToListAsync(),
                ImplementationSteps = await _context.ImplementationStep_
                    .Where(y => y.Id == id).ToListAsync(), 
            };
            return View(viewModel);
        }

        // ====================== CREATE SERVICE ======================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
                return View(service);

            await _context.Service_.AddAsync(service);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Service created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ====================== EDIT SERVICE ======================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _context.Service_.FirstOrDefaultAsync(x => x.Id == id);
            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var service = await _context.Service_.FirstOrDefaultAsync(x => x.Id == id);
            if (service == null)
                return NotFound();

            // تحديث البيانات
            service.Title = model.Title;
            service.Description = model.Description;
            service.IconUrl = model.IconUrl;
          

            _context.Service_.Update(service);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Service updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ====================== DELETE SERVICE ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Service_.FindAsync(id);
            if (service != null)
            {
                _context.Service_.Remove(service);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Service deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        // ===========================================================
        // =============== IMPLEMENTATION STEPS ======================
        // ===========================================================

        [HttpGet]
        public IActionResult CreateStep()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStep(ImplementationStep step)
        {
            if (!ModelState.IsValid)
                return View(step);

            await _context.ImplementationStep_.AddAsync(step);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Implementation step created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditStep(int id)
        {
            var step = await _context.ImplementationStep_.FirstOrDefaultAsync(x => x.Id == id);
            if (step == null)
                return NotFound();

            return View(step);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStep(int id, ImplementationStep model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var step = await _context.ImplementationStep_.FirstOrDefaultAsync(x => x.Id == id);
            if (step == null)
                return NotFound();

            step.StepTitle = model.StepTitle;
            step.Description = model.Description;
            step.Duration = model.Duration;
            step.StepNumber = model.StepNumber;

            _context.ImplementationStep_.Update(step);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Implementation step updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStep(int id)
        {
            var step = await _context.ImplementationStep_.FindAsync(id);
            if (step != null)
            {
                _context.ImplementationStep_.Remove(step);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Implementation step deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
