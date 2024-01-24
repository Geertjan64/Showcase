
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Showcase.Areas.Identity.Data;
using Showcase.Services;
using System.Threading.Tasks;
using Showcase.Models;

namespace Showcase.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameManager _gameManager;
        private readonly UserManager<ShowcaseUser> _userManager;
        private Player CurrentPlayer; 

        public GameHub(GameManager manager, UserManager<ShowcaseUser> userManager)
        {
            _gameManager = manager;
            _userManager = userManager;
        }

        public async Task CreateGame()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            CurrentPlayer = new Player(user.Id);
            _gameManager.CreateGame(CurrentPlayer);

            await Clients.Others.SendAsync("gameCreated", _gameManager.Game.Id);
            await Clients.Caller.SendAsync("startGame");
        }

        public async Task JoinGame(string gameId)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            CurrentPlayer = new Player(user.Id);
            _gameManager.JoinGame(gameId, CurrentPlayer);       

            await Clients.User(_gameManager.Game.Player1.Id).SendAsync("gameJoined", gameId, CurrentPlayer.Id);
        }

        public async Task MakeMove(int row, int col)
        {
            _gameManager.MakeMove(row, col, CurrentPlayer);
        }

        public async Task StartGame()
        {
            await Clients.All.SendAsync("startGame");
        }
    }
}
