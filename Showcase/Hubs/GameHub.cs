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
            string playerName = _gameManager.GetPlayer(Context.ConnectionId);

            if (playerName == null)
            {
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

            if (playerName != null)
            {
                await Clients.Group("GameGroup").SendAsync("Move", $"{playerName} made a move: {move}");

                // Send the move to the specific player who made the move
                string otherPlayerConnectionId = _gameManager.GetOtherPlayerConnectionId(Context.ConnectionId);
                if (otherPlayerConnectionId != null)
                {
                    await Clients.Client(otherPlayerConnectionId).SendAsync("Move", $"{playerName} made a move: {move}");
                    await Clients.All.SendAsync("UpdateGameboard", move);
                }
            }
        }
    }
}
