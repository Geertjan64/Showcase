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
               .Select(gr => new GameResultRecord
               {
                   GameId = gr.GameId,
                   Player1Id = gr.Player1Id,
                   Player2Id = gr.Player2Id,
                   Result = gr.Result,
                   DatePlayed = gr.DatePlayed
               })
               .ToList()
        }).ToList();



        return View(userWithGames);
    }

    [HttpGet]
    public async Task<IActionResult> CheckIfAdmin()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return Json(new { isAdmin = true });
        }

        return Json(new { isAdmin = false });
    }

}
