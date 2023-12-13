using Microsoft.AspNetCore.SignalR;
using Showcase.Services;
using System.Threading.Tasks;

namespace Showcase.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameManager _gameManager;

        public GameHub(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public async Task StartGame()
        {
            await Clients.All.SendAsync("GameStarted", _gameManager.GetCurrentPlayer());
        }
    }
}