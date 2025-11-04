using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammedPortfolio.Models.ViewModels;
using MohammedPortfolio.Models;
using MohammedPortfolio.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MohammedPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext db)
        {
            _context = db;
        }

        // ========================== INDEX ==============================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProjectViewModel
            {
                Projects = await _context.Project_
                    .Include(p => p.Category)
                    .ToListAsync(),
                Categories = await _context.Category_.ToListAsync(),
                Tools = await _context.Tool_.ToListAsync(),
                ProjectDetails = await _context.ProjectDetails_.ToListAsync(),
                ProjectImages = await _context.ProjectImage_.ToListAsync()
            };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> AllProject()
        {
            var viewModel = new ProjectViewModel
            {
                Projects = await _context.Project_.Include(p => p.Category).ToListAsync(),
            };
            return View(viewModel);

        }
        // ========================== CREATE PROJECT ==============================
        [HttpGet]
        public async Task<IActionResult> CreateProject()
        {
            var Categories = await _context.Category_.ToListAsync();

            ViewBag.Categories = Categories;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(ProjectCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var Categories = await _context.Category_.ToListAsync();
                ViewBag.Categories = Categories;
                return View(model);
            }

            // 1️⃣ إنشاء المشروع الأساسي
            var project = new Project
            {
                Title = model.Title.Trim(),
                Description = model.Description.Trim(),
                GitHubUrl = model.GitHubUrl,
                LiveDemoUrl = model.LiveDemoUrl,
                CategoryId = model.CategoryId
            };

            if (model.ImageFile != null)
            {
                using var memoryStream = new MemoryStream();
                await model.ImageFile.CopyToAsync(memoryStream);
                project.Image = memoryStream.ToArray();
            }

            await _context.Project_.AddAsync(project);
            await _context.SaveChangesAsync(); // نحتاج الـ ID

            // 2️⃣ إنشاء التفاصيل
            var detail = new ProjectDetails
            {
                ProjectId = project.Id,
                Overview = model.Overview
            };
            await _context.ProjectDetails_.AddAsync(detail);
            await _context.SaveChangesAsync();

            // 3️⃣ إنشاء الأدوات

            if (model.Tools != null && model.Tools.Count > 0)
            {
                foreach (var toolName in model.Tools)
                {
                    var tool = new Tool
                    {
                        ProjectDetailsId = detail.Id,
                        ToolName = toolName.Trim()
                    };
                    await _context.Tool_.AddAsync(tool);
                }
            }

            // 4️⃣ إنشاء الصور الإضافية
            if (model.AdditionalImages != null && model.AdditionalImages.Count > 0)
            {
                foreach (var file in model.AdditionalImages)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var image = new ProjectImage
                    {
                        ProjectDetailsId = detail.Id,
                        Image = ms.ToArray()
                    };
                    await _context.ProjectImage_.AddAsync(image);
                }
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = " تم إنشاء المشروع وكل التفاصيل بنجاح";
            return RedirectToAction(nameof(AllProject));
        }

        // ========================== EDIT PROJECT ==============================
        [HttpGet]
        public async Task<IActionResult> EditProject(int id)
        {
            var project = await _context.Project_
                .Include(p => p.ProjectDetails)
                .ThenInclude(d => d.Tools)
                .Include(p => p.ProjectDetails)
                .ThenInclude(d => d.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            var detail = project.ProjectDetails;


            var viewModel = new ProjectCreateViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                GitHubUrl = project.GitHubUrl,
                LiveDemoUrl = project.LiveDemoUrl,
                CategoryId = project.CategoryId,
                Overview = detail?.Overview,
                Tools = detail?.Tools?.Select(t => t.ToolName).ToList()
            };
            ViewBag.ProjectId = project.Id;
            ViewBag.Categories = new SelectList(await _context.Category_.ToListAsync(), "Id", "CategoryName");
            ViewBag.AdditionalImages = detail?.Images?
                .Select(i => new { i.Id, i.Image })
                .ToList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int id, ProjectCreateViewModel model)
        {
            var project = await _context.Project_
                .Include(p => p.ProjectDetails)
                    .ThenInclude(d => d.Tools)
                .Include(p => p.ProjectDetails)
                    .ThenInclude(d => d.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _context.Category_.ToListAsync(), "Id", "CategoryName");
                return View(model);
            }

            // تحديث البيانات الأساسية
            project.Title = model.Title?.Trim();
            project.Description = model.Description?.Trim();
            project.GitHubUrl = model.GitHubUrl?.Trim();
            project.LiveDemoUrl = model.LiveDemoUrl?.Trim();
            project.CategoryId = model.CategoryId;

            if (model.ImageFile != null)
            {
                using var memoryStream = new MemoryStream();
                await model.ImageFile.CopyToAsync(memoryStream);
                project.Image = memoryStream.ToArray();
            }

            var detail = project.ProjectDetails;

            if (detail != null)
            {
                detail.Overview = model.Overview;

                // تحديث الأدوات
                _context.Tool_.RemoveRange(detail.Tools);
                if (model.Tools != null)
                {
                    detail.Tools = model.Tools.Select(t => new Tool
                    {
                        ProjectDetailsId = detail.Id,
                        ToolName = t.Trim()
                    }).ToList();
                }

                // تحديث الصور الإضافية
                if (model.AdditionalImages != null && model.AdditionalImages.Any())
                {
                    foreach (var img in model.AdditionalImages)
                    {
                        using var ms = new MemoryStream();
                        await img.CopyToAsync(ms);
                        _context.ProjectImage_.Add(new ProjectImage
                        {
                            ProjectDetailsId = detail.Id,
                            Image = ms.ToArray()
                        });
                    }
                }
            }

           // _context.Update(project);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = " تم تعديل المشروع بنجاح";
            return RedirectToAction(nameof(AllProject));
        }

        // ========================== DELETE PROJECT ==============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Project_.FindAsync(id);
            if (project != null)
            {
                _context.Project_.Remove(project);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم حذف المشروع بنجاح ";
            }
            return RedirectToAction(nameof(AllProject));

        }
      
        [HttpPost]
        public async Task<IActionResult> DeleteProjectImage(int id)
        {
            var image = await _context.ProjectImage_.FindAsync(id);
            if (image == null)
                return NotFound();

            _context.ProjectImage_.Remove(image);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }



    }

}
