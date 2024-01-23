
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

        public GameHub(GameManager manager, UserManager<ShowcaseUser> userManager)
        {
            _gameManager = manager;
            _userManager = userManager;
        }

        public async Task CreateGame()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            Player player1 = new Player(user.Id, 1);
            _gameManager.CreateGame(player1);

            // possibly add to a list of games?
        }

        public async Task JoinGame()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            Player player2 = new Player(user.Id, 2); 
            _gameManager.JoinGame(player2);       

            await Clients.Caller.SendAsync("playerConnected", player2.Id);

            await Clients.AllExcept(Context.ConnectionId).SendAsync("playerConnected", user.Id);
        }

        public async Task MakeMove(int row, int col, Player player)
        {
            _gameManager.MakeMove(row, col, player);
        }

        public async Task StartGame()
        {
            await Clients.All.SendAsync("startGame");
        }
    }
}
