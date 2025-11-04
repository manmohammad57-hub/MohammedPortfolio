using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammedPortfolio.Data;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkillsController(ApplicationDbContext db)
        {
            _context = db;
        }

        // ================== عرض جميع المهارات ==================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var skills = await _context.Skill_.ToListAsync();
            return View(skills);
        }

        // ================== تفاصيل المهارة ==================
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var skill = await _context.Skill_.FirstOrDefaultAsync(x => x.Id == id);
            if (skill == null)
                return NotFound();

            return View(skill);
        }

        // ================== إنشاء مهارة جديدة (عرض الصفحة) ==================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ================== إنشاء مهارة جديدة (حفظ البيانات) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skil)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Skill_.AddAsync(skil);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "تمت إضافة المهارة بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
                return View(skil);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ أثناء إنشاء المهارة: {ex.Message}");
                return View(skil);
            }
        }

        // ================== تعديل المهارة (عرض الصفحة) ==================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var skill = await _context.Skill_.FindAsync(id);
            if (skill == null)
                return NotFound();

            return View(skill);
        }

        // ================== تعديل المهارة (حفظ التعديلات) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Skill model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(model);

                var skill = await _context.Skill_.FindAsync(id);
                if (skill == null)
                    return NotFound();

                // تحديث البيانات
                skill.SkillName = model.SkillName;
                skill.Description = model.Description;
                skill.Level = model.Level;

                _context.Update(skill);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم تحديث المهارة بنجاح.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ أثناء التعديل: {ex.Message}");
                return View(model);
            }
        }

        // ================== حذف المهارة ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleteskill = await _context.Skill_.FindAsync(id);
                if (deleteskill == null)
                    return NotFound();

                _context.Skill_.Remove(deleteskill);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم حذف المهارة بنجاح.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"حدث خطأ أثناء الحذف: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
