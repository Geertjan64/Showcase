using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Showcase.Hubs;
using Showcase.Services;

namespace Showcase.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly IHubContext<GameHub> _hubContext;
        private readonly GameManager _gameManager;

        public GameController(IHubContext<GameHub> hubContext, GameManager gameManager)
        {
            _hubContext = hubContext;
            _gameManager = gameManager;
        }

        [HttpPost("start")]
        public async Task<ActionResult<string>> StartGame()
        {
            await _hubContext.Clients.All.SendAsync("GameStarted", _gameManager.GetCurrentPlayer()); 
            return Ok("Game started");
        }

        [HttpPost("move")]
        public async Task<ActionResult<string>> Move([FromBody] string move)
        {
            await _hubContext.Clients.All.SendAsync("Move", move);
            return Ok("Move sent");
        }

    }
}
