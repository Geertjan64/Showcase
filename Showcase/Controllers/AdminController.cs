using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Showcase.Models;

namespace Showcase.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = _roleManager.CreateAsync(identityRole).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", "Contact");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}
