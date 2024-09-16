using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Showcase.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
    }
}
