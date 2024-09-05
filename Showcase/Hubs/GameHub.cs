﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Showcase.Areas.Identity.Data;
using Showcase.Services;
using System.Threading.Tasks;
using Showcase.Models;
using System.Numerics;
using Showcase.Data;
using Microsoft.EntityFrameworkCore;

namespace Showcase.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameManager _gameManager;
        private readonly UserManager<ShowcaseUser> _userManager;
        private readonly GameDbContext _gameDbContext;

        public GameHub(GameManager manager, UserManager<ShowcaseUser> userManager, GameDbContext gameDbContext)
        {
            _gameManager = manager;
            _userManager = userManager;
            _gameDbContext = gameDbContext;
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

        public async Task CheckIfGameIsInProgress()
        {
            if (_gameManager.CheckIfGameIsInProgress())
            {
                var user = await _userManager.GetUserAsync(Context.User);
                var player = _gameManager.GetPlayer(user.Id);
                var opponent = _gameManager.ReturnOpponent(player.Id);

                await Clients.User(opponent.Id).SendAsync("removeJoinGame", player.Id);
                await Clients.Caller.SendAsync("removeJoinGame", player.Id);
            }
        }

        public async Task CheckWinner()
        {
            var result = _gameManager.GetGameResult().ToString();

            await Clients.Others.SendAsync("gameOverMessage", result);
            await Clients.Caller.SendAsync("gameOverMessage", result);
        }

        public async Task SaveGame()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var player = _gameManager.GetPlayer(user.Id);
            var opponent = _gameManager.ReturnOpponent(player.Id);

            var gameResult = new GameResultRecord
            {
                GameId = _gameManager.Game.Id,
                Player1Id = _gameManager.Game.Player1.Id,
                Player2Id = _gameManager.Game.Player2.Id,
                Result = _gameManager.Game.GameResult,
                DatePlayed = DateTime.UtcNow
            };

            _gameDbContext.GameResults.Add(gameResult);
            await _gameDbContext.SaveChangesAsync();

            await Clients.User(opponent.Id).SendAsync("gameSaved", player.Id);
            await Clients.Caller.SendAsync("gameSaved", player.Id);
        }

        public async Task ResetGame()
        {
            _gameManager.ResetGame();
            await Clients.Others.SendAsync("gameReset");
            await Clients.Caller.SendAsync("gameReset");
        }

        public async Task GetGamesByUser()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var player = _gameManager.GetPlayer(user.Id);
            var opponent = _gameManager.ReturnOpponent(player.Id);

            var playerGames = await _gameDbContext.GameResults
                .Where(game => game.Player1Id == player.Id || game.Player2Id == player.Id)
                .ToListAsync();

            var opponentGames = await _gameDbContext.GameResults
                .Where(game => game.Player1Id == opponent.Id || game.Player2Id == opponent.Id)
                .ToListAsync();

            // Stuur de games naar de huidige speler
            await Clients.Caller.SendAsync("receiveGames", playerGames);

            // Stuur de games naar de tegenstander
            await Clients.User(opponent.Id).SendAsync("receiveGames", opponentGames);
        }
    }
}
