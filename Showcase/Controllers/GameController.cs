// GameController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Showcase.Hubs;
using Showcase.Services;
using System.Threading.Tasks;

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
    }
}
