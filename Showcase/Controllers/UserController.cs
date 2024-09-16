using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Showcase.Areas.Identity.Data;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<ShowcaseUser> _userManager;

    public UserController(UserManager<ShowcaseUser> userManager)
    {
        _userManager = userManager;
    }

    // Methode om alle geregistreerde gebruikers op te halen
    public IActionResult ListUsers()
    {
        var users = _userManager.Users;
        return View(users);
    }
}
