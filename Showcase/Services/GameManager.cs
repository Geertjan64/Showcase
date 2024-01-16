using Microsoft.AspNetCore.SignalR;
using Showcase.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {
        private readonly IHubContext<GameHub> hubContext;
        private readonly Dictionary<string, string> playerConnections = new Dictionary<string, string>();
        private readonly Dictionary<string, char> playerSymbols = new Dictionary<string, char>();
        private readonly char[,] board = new char[3, 3];
        private string currentPlayer;

        public GameManager(IHubContext<GameHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public void AddPlayer(string connectionId)
        {
            playerConnections.Add(connectionId, connectionId);

            if (playerConnections.Count == 1)
            {
                playerSymbols.Add(connectionId, 'X');
                currentPlayer = connectionId;
                NotifyPlayerConnected(connectionId, 'X');
            }
            else if (playerConnections.Count == 2)
            {
                playerSymbols.Add(connectionId, 'O');
                NotifyPlayerConnected(connectionId, 'O');
                NotifyStartGame();
            }
        }

        public void MakeMove(string connectionId, int row, int col)
        {
            // ...

            if (CheckForWinner() || CheckForTie())
            {
                NotifyGameEnd();
                ResetGame();
            }
            else
            {
                currentPlayer = playerConnections.Values.First(id => id != connectionId);
            }
        }

        private void NotifyPlayerConnected(string connectionId, char symbol)
        {
            hubContext.Clients.All.SendAsync("playerConnected", connectionId, symbol);
        }

        private void NotifyPlayerDisconnected(string connectionId)
        {
            hubContext.Clients.All.SendAsync("playerDisconnected", connectionId);
        }

        private void NotifyStartGame()
        {
            hubContext.Clients.All.SendAsync("startGame");
        }

        private void NotifyMove(string connectionId, int row, int col)
        {
            hubContext.Clients.All.SendAsync("moveMade", connectionId, row, col);
        }

        private void NotifyGameEnd()
        {
            hubContext.Clients.All.SendAsync("gameEnd");
        }

        private bool CheckForWinner()
        {
            // Implementeer de logica om te controleren op een winnaar
            // Return true als er een winnaar is, anders false
            return false;
        }

        private bool CheckForTie()
        {
            // Implementeer de logica om te controleren op een gelijkspel
            // Return true als het een gelijkspel is, anders false
            return false;
        }

        private void ResetGame()
        {
            // Implementeer de logica om het spel te resetten
            // Dit kan bijvoorbeeld het bord en de huidige speler opnieuw instellen
        }
    }

}
