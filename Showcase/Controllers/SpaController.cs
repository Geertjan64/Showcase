using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Showcase.Controllers
{
    public class SpaController : Controller
    {
        [Authorize]
        public IActionResult Game()
        {
            return View();
        }
    }
}
