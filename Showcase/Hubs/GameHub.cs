using Microsoft.AspNetCore.SignalR;
using Showcase.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Showcase.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameManager _gameManager;
        private readonly List<string> _rooms = new List<string>();

        public GameHub(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public async Task CreateGroup()
        {
            string? playerName = _gameManager.GetPlayer(Context.ConnectionId);

            if (playerName == null)
            {
                Console.WriteLine("Player name is null");
                playerName = _gameManager.AssignPlayer(Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, "GameGroup");
                await Clients.Caller.SendAsync("GroupCreated", playerName);
                await Clients.OthersInGroup("GameGroup").SendAsync("GroupJoined", playerName);
            }
        }

        public async Task StartGame()
        {
            await Clients.Group("GameGroup").SendAsync("GameStarted", _gameManager.GetNextPlayer());
        }

        public async Task MakeMove(string move)
        {
            string playerName = _gameManager.GetPlayer(Context.ConnectionId);

            if (playerName != null && playerName == _gameManager.GetNextPlayer())
            {
                await Clients.Group("GameGroup").SendAsync("Move", $"{playerName} made a move: {move}");

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
