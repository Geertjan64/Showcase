using Microsoft.AspNetCore.SignalR;
using Showcase.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Showcase.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameManager gameManager;

        public GameHub(GameManager manager)
        {
            gameManager = manager;
        }

        public override async Task OnConnectedAsync()
        {
            gameManager.AddPlayer(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public void MakeMove(int row, int col)
        {
            gameManager.MakeMove(Context.ConnectionId, row, col);
        }
    }
}
