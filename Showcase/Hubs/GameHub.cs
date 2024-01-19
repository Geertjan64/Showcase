// GameHub.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Showcase.Areas.Identity.Data;
using Showcase.Services;
using System.Threading.Tasks;

namespace Showcase.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameManager gameManager;
        private readonly UserManager<ShowcaseUser> userManager;

        public GameHub(GameManager manager, UserManager<ShowcaseUser> userManager)
        {
            gameManager = manager;
            this.userManager = userManager;
        }

        public async Task AddPlayer()
        {
            var user = await userManager.GetUserAsync(Context.User);
            char playerSymbol = gameManager.AddPlayer(user.Id);

            // Notify the player that they have been added
            await Clients.Caller.SendAsync("playerConnected", user.Id, playerSymbol);

            // Notify all connected clients that a player has been added
            await Clients.AllExcept(Context.ConnectionId).SendAsync("playerConnected", user.Id, playerSymbol);

            // If there are two players added, start the game
            if (gameManager.GetPlayerCount() == 2)
            {
                await StartGame();
            }
        }

        public async Task StartGame()

        public async Task StartGame()
        {
            // Add the logic to start the game here
            await Clients.All.SendAsync("startGame");
        }

        public async Task MakeMove(int row, int col, char symbol)
        {
            // Voeg de logica toe om de zet van een speler te verwerken
            var currentPlayerId = Context.UserIdentifier;
            if (playerName != null && playerName == _gameManager.GetNextPlayer())
            {
                await Clients.Group("GameGroup").SendAsync("Move", $"{playerName} made a move: {move}");
            {
                // Stel de volgende speler in
                _gameManager.SwitchPlayer();

                // Stuur het spelbord alleen naar de specifieke speler die de zet heeft gedaan
                await Clients.Client(Context.ConnectionId).SendAsync("UpdateGameboard", move);

                // Stuur de bijgewerkte spelstatus naar alle verbonden clients
                await Clients.All.SendAsync("UpdateGameStatus", _gameManager.GetNextPlayer());
            }
                }

            }
        }
    }
}
