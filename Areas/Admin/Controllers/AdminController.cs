using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //  Show all users
        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        //  Promote user to Admin
        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            await _userManager.AddToRoleAsync(user, "Admin");

            TempData["Success"] = $"{user.UserName} has been promoted to Admin.";
            return RedirectToAction("Users");
        }
        //  Disable user to Admin
        [HttpPost]
        public async Task<IActionResult> DisableAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                TempData["Success"] = $"Admin rights removed from {user.UserName}.";
            }
            return RedirectToAction("Users");
        }


        //  Delete user
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);

            TempData["Success"] = $"User {user.UserName} deleted successfully.";
            return RedirectToAction("Users");
        }
    }
}
