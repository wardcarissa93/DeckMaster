using DeckMaster.Data;
using DeckMaster.Repositories;
using DeckMaster.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace DeckMaster.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            UserRepo userRepo = new UserRepo(_context);
            IEnumerable<UserVM> users = userRepo.GetAllUsers();

            return View(users);
        }

        public async Task<IActionResult> Detail(string userName, string message = "")
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager, _context);

            var roles = await userRoleRepo.GetUserRolesAsync(userName);

            ViewBag.Message = message;
            ViewBag.UserName = userName;
            ViewBag.FullName = await userRoleRepo.GetUserFullNameAsync(userName);

            return View(roles);
        }

        public ActionResult Create()
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            ViewBag.RoleSelectList = roleRepo.GetRoleSelectList();

            UserRepo userRepo = new UserRepo(_context);
            ViewBag.UserSelectList = userRepo.GetUserSelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Added for CSRF protection
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager, _context);

            if (ModelState.IsValid)
            {
                var result = await userRoleRepo.CreateUserRoleAsync(userRoleVM.Email, userRoleVM.RoleName);

                if (result.Success)
                {
                    string message = $"{userRoleVM.RoleName} permissions successfully added to {userRoleVM.Email}.";
                    return RedirectToAction("Detail", "UserRole", new { userName = userRoleVM.Email, message = message });
                }

                ModelState.AddModelError("", result.ErrorMessage);
            }

            RoleRepo roleRepo = new RoleRepo(_context);
            ViewBag.RoleSelectList = roleRepo.GetRoleSelectList();

            UserRepo userRepo = new UserRepo(_context);
            ViewBag.UserSelectList = userRepo.GetUserSelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Added for CSRF protection
        public async Task<IActionResult> Delete(UserRoleVM userRoleVM)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager, _context);

            var result = await userRoleRepo.RemoveUserRoleAsync(userRoleVM.Email, userRoleVM.RoleName);

            var roles = await userRoleRepo.GetUserRolesAsync(userRoleVM.Email);

            ViewBag.Message = result.Success ? "UserRole removed successfully." : result.ErrorMessage;
            ViewBag.UserName = userRoleVM.Email;

            return View("Detail", roles); // Ensure the correct view is returned
        }
    }
}
