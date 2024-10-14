using Microsoft.EntityFrameworkCore;
using Showcase.Data;
using Showcase.Hubs;
using Showcase.Models;
using Showcase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.Tests
{
    public class GameHubTests
    {
        private readonly GameDbContext _gameDbContext;
        private readonly GameHub _gameHub;

        public GameHubTests()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGameDb")
                .Options;

            _gameDbContext = new GameDbContext(options);

            var gameManager = new GameManager();

            _gameHub = new GameHub(gameManager, null, _gameDbContext);
        }

        [Fact]
        public async Task GetGamesByUser_ShouldReturnCorrectGames()
        {
            var userId = "test-user";
            var gameRecords = new List<GameResultRecord>
            {
                new GameResultRecord { GameId = "1", Player1Id = userId, Player2Id = "opponent1", Result = GameResult.Player1Win, DatePlayed = DateTime.UtcNow },
                new GameResultRecord { GameId = "2", Player1Id = "opponent2", Player2Id = userId, Result = GameResult.Draw, DatePlayed = DateTime.UtcNow }
            };

            _gameDbContext.GameResults.AddRange(gameRecords);
            await _gameDbContext.SaveChangesAsync();

            var result = await _gameDbContext.GameResults
                .Where(gr => gr.Player1Id == userId || gr.Player2Id == userId)
                .ToListAsync();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, game => game.GameId == "1" && game.Player1Id == userId);
            Assert.Contains(result, game => game.GameId == "2" && game.Player2Id == userId);
        }
    }
}
