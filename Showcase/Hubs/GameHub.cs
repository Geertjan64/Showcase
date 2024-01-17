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

        private async Task StartGame()
        {
            // Add the logic to start the game here
            await Clients.All.SendAsync("startGame");
        }

        public async Task MakeMove(int row, int col, char symbol)
        {
            // Voeg de logica toe om de zet van een speler te verwerken
            var currentPlayerId = Context.UserIdentifier;
            var otherPlayerId = gameManager.GetOtherPlayerId(currentPlayerId);

            if (gameManager.IsPlayerTurn(currentPlayerId))
            {
                if (gameManager.MakeMove(row, col, symbol))
                {
                    // Stuur de zet naar beide spelers
                    await Clients.Client(currentPlayerId).SendAsync("updateCell", row, col, symbol);
                    await Clients.Client(otherPlayerId).SendAsync("updateCell", row, col, symbol);

                    // Controleer of het spel is afgelopen na deze zet
                    var winner = gameManager.CheckForWinner();
                    if (winner != '\0')
                    {
                        // Stuur een bericht naar beide spelers dat het spel is afgelopen
                        await Clients.Client(currentPlayerId).SendAsync("gameOver", winner);
                        await Clients.Client(otherPlayerId).SendAsync("gameOver", winner);
                    }
                    else if (gameManager.IsBoardFull())
                    {
                        // Als het bord vol is en er is geen winnaar, stuur een bericht naar beide spelers dat het een gelijkspel is
                        await Clients.Client(currentPlayerId).SendAsync("gameOver", null);
                        await Clients.Client(otherPlayerId).SendAsync("gameOver", null);
                    }
                    else
                    {
                        // Wissel van beurt
                        gameManager.SwitchTurn();
                    }
                }
            }
        }

        public async Task RestartGame()
        {
            // Voeg de logica toe om het spel opnieuw te starten
            var currentPlayerId = Context.UserIdentifier;
            var otherPlayerId = gameManager.GetOtherPlayerId(currentPlayerId);

            // Stuur het bericht naar beide spelers om het spel te herstarten
            await Clients.Client(currentPlayerId).SendAsync("startGame");
            await Clients.Client(otherPlayerId).SendAsync("startGame");

            // Reset het spel in de GameManager
            gameManager.ResetGame();
        }

        // ... (other existing code)
    }
}
