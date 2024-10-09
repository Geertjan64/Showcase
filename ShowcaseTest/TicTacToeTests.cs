using Showcase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowcaseTest
{
    public class TicTacToeTests
    {
        [Fact]
        public void Constructor_ShouldInitializeGame_WithStartingPlayer()
        {
            var player1 = new Player("player1Id", "Test1", "Test1", "test1@example.com");

            var game = new TicTacToe(player1);

            Assert.NotNull(game);
            Assert.Equal(player1, game.Player1);
            Assert.Equal(1, player1.Symbol);
            Assert.Equal(GameState.WaitingForSecondPlayer, game.GameState);
        }

        [Fact]
        public void JoinGame_ShouldStartGame_WhenSecondPlayerJoins()
        {
            var player1 = new Player("player1Id", "Test1", "Test1", "test1@example.com");
            var player2 = new Player("player2Id", "Test2", "Test2", "test2@example.com");
            var game = new TicTacToe(player1);

            game.JoinGame(game.Id, player2);

            Assert.NotNull(game.Player2);
            Assert.Equal(player2, game.Player2);
            Assert.Equal(GameState.IsInProgress, game.GameState);
        }

        [Fact]
        public void MakeMove_ShouldReturnTrue_WhenMoveIsValid()
        {
            var player1 = new Player("player1Id", "Test1", "Test1", "test1@example.com");
            var player2 = new Player("player2Id", "Test2", "Test2", "test2@example.com");
            var game = new TicTacToe(player1);
            game.JoinGame(game.Id, player2);
            game.Turn = Turn.Player1;

            var result = game.MakeMove(0, 0, player1);

            Assert.True(result);
            Assert.Equal(1, game.Board[0, 0]);
            Assert.Equal(Turn.Player2, game.Turn);
        }

        [Fact]
        public void CheckWinner_ShouldSetGameResult_WhenPlayerWins()
        {
            var player1 = new Player("player1Id", "Test1", "Test1", "test1@example.com");
            var player2 = new Player("player2Id", "Test2", "Test2", "test2@example.com");
            var game = new TicTacToe(player1);
            game.JoinGame(game.Id, player2);

            game.Board[0, 0] = 1;
            game.Board[0, 1] = 1;
            game.Board[0, 2] = 1;

            game.CheckGameState();

            Assert.Equal(GameResult.Player1Win, game.GameResult);
            Assert.Equal(GameState.Finished, game.GameState);
        }




    }
}
