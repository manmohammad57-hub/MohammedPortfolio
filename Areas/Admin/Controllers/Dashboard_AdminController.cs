using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammedPortfolio.Data;
using MohammedPortfolio.Models;

namespace MY_Protfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class Dashboard_AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public Dashboard_AdminController(ApplicationDbContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            // Get the total counts
            var totalProjects = _context.Project_.Count();
            var totalServices = _context.Service_.Count();

            // Send to view
            ViewBag.TotalProjects = totalProjects;
            ViewBag.TotalServices = totalServices;

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AboutDashboard()
        {
            var about = await _context.About_.FirstOrDefaultAsync();
            return View(about);

            
        }

        [HttpGet]
        public async Task<IActionResult> EditAbout(int id)
        {
            var about = await _context.About_.FindAsync(id);
            if (about == null)
                return NotFound();

            return View(about); // إرسال البيانات للعرض في النموذج
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbout(About model, IFormFile? AboutImageFile, IFormFile? ResumeFile)
        {
            if (!ModelState.IsValid)
                return View(model);

            // البحث عن السجل القديم
            var existingAbout = await _context.About_.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (existingAbout == null)
                return NotFound();

            // تحديث الحقول النصية
            existingAbout.Tagline = model.Tagline?.Trim();
            existingAbout.Description = model.Description?.Trim();

            // تحديث الصورة إن وُجدت
            if (AboutImageFile != null && AboutImageFile.Length > 0)
            {
                using var stream = new MemoryStream();
                await AboutImageFile.CopyToAsync(stream);
                existingAbout.Aboutimage = stream.ToArray();
            }

            // تحديث ملف الـ Resume إن وُجد
            if (ResumeFile != null && ResumeFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await ResumeFile.CopyToAsync(ms);
                existingAbout.ResumePdf = ms.ToArray();
            }

            // حفظ التغييرات
            _context.About_.Update(existingAbout);
            await _context.SaveChangesAsync();

            return RedirectToAction("AboutDashboard", "Dashboard_Admin", new { area = "Admin" });
        }


        //---------------------
        [HttpGet]
        public async Task<IActionResult> Category()
        {
            var Categories = await _context.Category_.ToListAsync();
            return View(Categories);
        }

        [HttpGet]
        public IActionResult CreateCategroy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategroy( Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Category_.AddAsync(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Category));
                }
                return View(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ أثناء إنشاء المهارة: {ex.Message}");
                return View(category);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditCategroy(int id)
        {
            var category = await _context.Category_.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // ================== تعديل المهارة (حفظ التعديلات) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategroy(int id, Category model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(model);

                var category = await _context.Category_.FindAsync(id);
                if (category == null)
                    return NotFound();

                // تحديث البيانات
                category.CategoryName = model.CategoryName;

                _context.Update(category);
                await _context.SaveChangesAsync();

                return RedirectToAction("Category", "Dashboard_Admin", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Category_.FindAsync(id);
                if (category == null)
                    return NotFound();

                _context.Category_.Remove(category);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Projects", new { area = "Admin" });
            }
        }

        // عرض السيرة الذاتية في المتصفح مباشرة
        [HttpGet]
        public async Task<IActionResult> ViewResume(int id)
        {
            var Resume = await _context.About_.FirstOrDefaultAsync();
            if (Resume == null || Resume.ResumePdf == null)
            {
                return NotFound("Resume not found");
            }

            return File(Resume.ResumePdf, "application/pdf");
        }

        // تحميل السيرة الذاتية كملف
        [HttpGet]
        public async Task<IActionResult> DownloadResume(int id)
        {
            var Resume = await _context.About_.FirstOrDefaultAsync(p => p.Id == id);
            var info = await _context.Profile_.FirstOrDefaultAsync();
            if (Resume == null || Resume.ResumePdf == null)
            {
                return NotFound("Resume not found");
            }

            var fileName = $"{info.FirstName}_{info.LastName}_Resume.pdf";
            return File(Resume.ResumePdf, "application/pdf", fileName);
        }

        //Bio
        [HttpGet]
        public async Task<IActionResult> Bio()
        {
            var bios =await _context.Bio_.ToListAsync();    
            return View(bios);
        }

        [HttpGet]
        public IActionResult CreateBio()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBio(Bio model)
        {
            if (!ModelState.IsValid)
            {
                // Return the same view with validation messages
                return View(model);
            }

            // Trim whitespace safely
            model.NameBoi = model.NameBoi?.Trim();
            model.Content = model.Content?.Trim();

            // Explicitly set foreign keys only if needed (usually EF handles these automatically)
            // model.SkillId = model.SkillId;
            // model.ServiceId = model.ServiceId;
            // model.AboutId = model.AboutId;

            try
            {
                _context.Bio_.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Bio", "Dashboard_Admin", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                // Log the exception (optional but recommended)
                // _logger.LogError(ex, "Error creating Bio");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditBio(int id)
        {
            // Find the Bio by ID
            var bio = await _context.Bio_.FindAsync(id);
            if (bio == null)
            {
                return NotFound();
            }

            return View(bio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBio(int id, Bio model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Trim values
            model.NameBoi = model.NameBoi?.Trim();
            model.Content = model.Content?.Trim();

            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Bio", "Dashboard_Admin", new { area = "Admin" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bio_.Any(b => b.Id == id))
                    return NotFound();

                throw; // rethrow if not a missing record
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBio(int id)
        {
            try
            {
                var bio = await _context.Bio_.FindAsync(id);
                if (bio == null)
                    return NotFound();

                _context.Bio_.Remove(bio);
                await _context.SaveChangesAsync();

                return RedirectToAction("Bio", "Dashboard_Admin", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Bio", "Dashboard_Admin", new { area = "Admin" });
            }
        }



        public IActionResult Settings()
        {
            return View();
        }
       
   
      
    }
}
