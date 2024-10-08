using Moq;
using Showcase.Models;
using Showcase.Services;
using System.Numerics;
using Xunit;

namespace Showcase.Tests
{
    public class GameManagerTests
    {
        private readonly GameManager _gameManager;

        public GameManagerTests()
        {
            _gameManager = new GameManager();
        }

        [Fact]
        public void GetPlayer_ShouldReturnCorrectPlayer_WhenPlayerExists()
        {
            var player1 = new Player("player1Id", "Test1", "Test1", "test1@example.com");
            var player2 = new Player("player2Id", "Test2", "Test2", "test2@example.com");
            _gameManager.Game = new TicTacToe(player1);
            _gameManager.Game.Player2 = player2;

            var result = _gameManager.GetPlayer("player1Id");

            Assert.Equal(player1, result);
        }

        [Fact]
        public void ReturnOpponent_ShouldReturnCorrectOpponent()
        {
            var player1 = new Player("player1Id", "Test1", "Test1", "test1@example.com");
            var player2 = new Player("player2Id", "Test2", "Test2", "test2@example.com");

            _gameManager.Game = new TicTacToe(player1);
            _gameManager.Game.Player2 = player2;

            var result = _gameManager.ReturnOpponent("player1Id");

            Assert.Equal(player2, result);
        }

    }
}
