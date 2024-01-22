
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

        public List<Player> Players { get; set; } = new List<Player>();

        public GameHub(GameManager manager, UserManager<ShowcaseUser> userManager)
        {
            _gameManager = manager;
            _userManager = userManager;
        }

        public async Task AddPlayer()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            char playerSymbol = _gameManager.AddPlayer(user.Id);

            Players.Add(new Player(user.Id, playerSymbol));

            // Notify the player that they have been added
            await Clients.Caller.SendAsync("playerConnected", user.Id, playerSymbol);

            // Notify all connected clients that a player has been added
            await Clients.AllExcept(Context.ConnectionId).SendAsync("playerConnected", user.Id, playerSymbol);

            if (_gameManager.GetPlayerCount() == 1)
            {
                Console.WriteLine(user.Id);
            }

            // If there are two players added, start the game
            if (_gameManager.GetPlayerCount() == 2)
            {
                await StartGame();
            }
        }

        public async Task StartGame()
        {
            // Add the logic to start the game here
            await Clients.All.SendAsync("startGame");
        }
    }
}
