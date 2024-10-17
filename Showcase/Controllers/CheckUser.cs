using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Showcase.Areas.Identity.Data;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CheckUser : ControllerBase
{
    private readonly UserManager<ShowcaseUser> _userManager;

    public CheckUser(UserManager<ShowcaseUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("current")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new { userId = user.Id, email = user.Email });
    }
}
