using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext.Entities;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            var viewModel = users.Select(user => new UserWithRolesViewModel
            {
                User = user,
                Roles = _userManager.GetRolesAsync(user).Result.ToList() // await olmadan Result ilə
            }).ToList();

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var viewModel = new EditUserRoleViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                CurrentRoles = currentRoles?.ToList() ?? new List<string>(),
                AllRoles = allRoles
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, string selectedRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, selectedRole);

            return RedirectToAction(nameof(Index));
        }
    }

}
