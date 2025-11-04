using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammedPortfolio.Data;
using MohammedPortfolio.Models;
using MohammedPortfolio.Models.ViewModels;
using System.Diagnostics;

namespace MohammedPortfolio.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, EmailService emailService)
        {
            _logger = logger;
            _context = db;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                Profile = await _context.Profile_.FirstOrDefaultAsync(),
                Bio = await _context.Bio_
                    .Where(b => b.NameBoi == "Home")
                    .FirstOrDefaultAsync(),
                Categories = await _context.Category_.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            var viewModel = new AboutViewModel
            {
                Profile = await _context.Profile_.FirstOrDefaultAsync(),
                About = await _context.About_.FirstOrDefaultAsync(),
                Skills = await _context.Skill_.ToListAsync(),
                Bio= await _context.Bio_.ToListAsync()
                
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            ViewBag.InfoUser = await _context.Profile_.FirstOrDefaultAsync();
            ViewBag.ContactDetails = await _context.ContactForm_.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactForm model)
        {
            //  إعادة تحميل البيانات بعد الإرسال
            ViewBag.InfoUser = await _context.Profile_.FirstOrDefaultAsync();
            ViewBag.ContactDetails = await _context.ContactForm_.ToListAsync();

            if (!ModelState.IsValid)
                return View(model);

            string messageBody = $@"
                <h3>New Message from Contact Form</h3>
                <p><strong>Name:</strong> {model.FullName}</p>
                <p><strong>Email:</strong> {model.Email}</p>
                <p><strong>Subject:</strong> {model.Subject}</p>
                <p><strong>Message:</strong></p>
                <p>{model.Message}</p>";

            try
            {
                await _emailService.SendEmailAsync(
                    "binsalmanmohammad57@gmail.com",
                    $"Contact Form: {model.Subject}",
                    messageBody,
                    model.Email, // بريد المُرسل
                    model.FullName // اسم المُرسل
                );

                ViewBag.Success = "✅ تم إرسال رسالتك بنجاح!";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"❌ فشل في إرسال البريد: {ex.Message}";
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Service()
        {
            var viewModel = new ServiceViewModel
            {
                Services = await _context.Service_.ToListAsync(),
                ImplementationSteps = await _context.ImplementationStep_.ToListAsync(),
                Bio = await _context.Bio_
                    .Where(b => b.NameBoi == "Service")
                    .FirstOrDefaultAsync(),
                ProjectForms = await _context.ProjectForm_.ToListAsync(),
                Categories = await _context.Category_.ToListAsync(),
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Service(ProjectForm model)
        {
            var viewModel = new ServiceViewModel
            {
                Services = await _context.Service_.ToListAsync(),
                ImplementationSteps = await _context.ImplementationStep_.ToListAsync(),
                ProjectForms = await _context.ProjectForm_.ToListAsync(),
                ProjectForm = model
            };

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "⚠️ Please fill all fields correctly.";
                return View(viewModel);
            }

            string messageBody = $@"
                <h3>New Project Request</h3>
                <p><strong>Full Name:</strong> {model.FullName}</p>
                <p><strong>Email:</strong> {model.Email}</p>
                <p><strong>Phone:</strong> {model.Phone}</p>
                <p><strong>Project Type:</strong> {model.ProjectType}</p>
                <p><strong>Description:</strong></p>
                <p>{model.Description}</p>";

            try
            {
                await _emailService.SendEmailAsync(
                    "binsalmanmohammad57@gmail.com",
                    $"New Project Request: {model.ProjectType}",
                    messageBody,
                    model.Email,
                    model.FullName
                );

                ViewBag.Success = "✅ Your project request has been sent successfully!";
                ModelState.Clear();
                viewModel.ProjectForm = new ProjectForm(); // إفراغ الحقول بعد الإرسال الناجح
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"❌ Error sending email: {ex.Message}";
            }

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Portfolio() // ==> Project Page 
        {
            var viewModel = new ProjectViewModel
            {
                Projects = await _context.Project_.ToListAsync(),
                Categories = await _context.Category_.ToListAsync(),
                ProjectImages = await _context.ProjectImage_.ToListAsync(),
                Bio = await _context.Bio_
                    .Where(b => b.NameBoi == "Project")
                    .FirstOrDefaultAsync(),
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectDetails(int id)
        {
            // اجلب المشروع مع تفاصيله
            var project = await _context.Project_
                .Include(p => p.ProjectDetails)
                    .ThenInclude(d => d.Tools)
                .Include(p => p.ProjectDetails)
                    .ThenInclude(d => d.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            // جهز الـ ViewModel
            var viewModel = new ProjectViewModel
            {
                Projects = new List<Project> { project },
                ProjectDetails = new List<ProjectDetails> { project.ProjectDetails },
                Tools = project.ProjectDetails?.Tools?.ToList(),
                ProjectImages = project.ProjectDetails?.Images?.ToList()
            };

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
