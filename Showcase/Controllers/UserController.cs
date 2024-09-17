using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Showcase.Areas.Identity.Data;
using Showcase.Data;
using Showcase.Models;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<ShowcaseUser> _userManager;
    private readonly GameDbContext _gameDbContext;

    public UserController(UserManager<ShowcaseUser> userManager, GameDbContext gameDbContext)
    {
        _userManager = userManager;
        _gameDbContext = gameDbContext;
    }

    // Methode om alle geregistreerde gebruikers op te halen
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListUsers()
    {
        var users = await _userManager.Users.ToListAsync();

        var userWithGames = users.Select(user => new UserWithGamesViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PlayedGames = _gameDbContext.GameResults
                .Where(gr => gr.Player1Id == user.Id || gr.Player2Id == user.Id)
                .ToList()
        }).ToList();

        return View(userWithGames);
    }


}
