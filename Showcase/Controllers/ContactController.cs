using Microsoft.AspNetCore.Mvc;

namespace Showcase.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ContactForm(string name)
        {
            ViewBag.ContactPerson = name;
            return View();
        }
    }
}
