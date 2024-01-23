using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Showcase.Controllers
{
    public class GameController : Controller
    {
        [Authorize]
        public IActionResult Game()
        {
            return View();
        }
    }
}
