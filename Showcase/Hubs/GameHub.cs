using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Showcase.Areas.Identity.Data;
using Showcase.Services;
using System.Threading.Tasks;
using Showcase.Models;
using System.Numerics;

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
            var player = new Player(user.Id);
            _gameManager.CreateGame(player);

            await Clients.Others.SendAsync("gameCreated", _gameManager.Game.Id);
            await Clients.Caller.SendAsync("startGame");
        }

        public async Task JoinGame(string gameId)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var player = new Player(user.Id);
            _gameManager.JoinGame(gameId, player);       

            await Clients.User(_gameManager.Game.Player1.Id).SendAsync("gameJoined", gameId, player.Id);
            await Clients.Caller.SendAsync("startGame");
        }

        public async Task MakeMove(int row, int col)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var player = _gameManager.GetPlayer(user.Id);
            var opponent = _gameManager.ReturnOpponent(player.Id);

            if (_gameManager.MakeMove(row, col, player))
            {
                await Clients.User(opponent.Id).SendAsync("updateCell", row, col, player.Symbol);
                await Clients.Caller.SendAsync("updateCell", row, col, player.Symbol);
            }

            await CheckIfGameIsOver();
        }

        public async Task CheckIfGameIsOver()
        {
            if (_gameManager.CheckIfGameIsOver())
            {
                var user = await _userManager.GetUserAsync(Context.User);
                var player = _gameManager.GetPlayer(user.Id);
                var opponent = _gameManager.ReturnOpponent(player.Id);

                await Clients.User(opponent.Id).SendAsync("gameOver", player.Id);
                await Clients.Caller.SendAsync("gameOver", player.Id);
            }
        }

        public async Task CheckWinner()
        {
            var result = _gameManager.GetGameResult().ToString();

            await Clients.Others.SendAsync("gameOverMessage", result);
            await Clients.Caller.SendAsync("gameOverMessage", result);
        }
    }
}
