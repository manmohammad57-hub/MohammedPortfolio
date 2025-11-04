using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MohammedPortfolio.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost()
        {
            // تسجيل الخروج بشكل صحيح
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");

            // إعادة التوجيه إلى صفحة MVC عادية لتجنب مشاكل Identity
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

        }
    }
}