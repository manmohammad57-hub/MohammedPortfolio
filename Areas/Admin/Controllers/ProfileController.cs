using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammedPortfolio.Data;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext db)
        {
            _context = db;
        }

        // ====================== عرض بيانات الأدمن ======================
        [HttpGet]
        public async Task<IActionResult> InfoAdmin()
        {
            var profile = await _context.Profile_.FirstOrDefaultAsync();
            if (profile == null)
            {
                // في حال لم يوجد سجل في قاعدة البيانات
                return RedirectToAction(nameof(CreateProfile));
            }

            return View(profile);
        }

        // ====================== عرض صفحة التعديل ======================
        [HttpGet]
        public async Task<IActionResult> EditProfile(int id)
        {
            var profile = await _context.Profile_.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // ====================== تنفيذ التعديل ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(Profile model, IFormFile? ProfileImageFile, IFormFile? ResumeFile)
        {
            if (!ModelState.IsValid)
                return View(model);

            var profile = await _context.Profile_.FirstOrDefaultAsync(p => p.Id == model.Id);
            if (profile == null)
                return NotFound();

            // تحديث البيانات النصية
            profile.FirstName = model.FirstName?.Trim();
            profile.LastName = model.LastName?.Trim();
            profile.Email = model.Email?.Trim();
            profile.Phone = model.Phone?.Trim();
            profile.Location = model.Location?.Trim();

            // تحديث الصورة إن وُجدت
            if (ProfileImageFile != null && ProfileImageFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await ProfileImageFile.CopyToAsync(ms);
                profile.ProfileImage = ms.ToArray();
            }

            

            _context.Profile_.Update(profile);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم تحديث الملف الشخصي بنجاح ✅";
            return RedirectToAction(nameof(InfoAdmin));
        }

        // ====================== صفحة إنشاء الملف الشخصي ======================
        [HttpGet]
        public IActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfile(Profile model, IFormFile? ProfileImageFile, IFormFile? ResumeFile)
        {
            if (!ModelState.IsValid)
                return View(model);

            // تحميل الصورة (اختياري)
            if (ProfileImageFile != null && ProfileImageFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await ProfileImageFile.CopyToAsync(ms);
                model.ProfileImage = ms.ToArray();
            }

        

            _context.Profile_.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم إنشاء الملف الشخصي بنجاح ✅";
            return RedirectToAction(nameof(InfoAdmin));
        }
    }
}
