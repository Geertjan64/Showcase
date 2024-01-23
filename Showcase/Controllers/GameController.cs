using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Showcase.Controllers
{
    [Authorize]
    [Route("api/Game")]
    [ApiController]
    public class GameController : Controller
    {
        public IActionResult Game()
        {
            return View();
        }
    }
}
