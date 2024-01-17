// GameManager.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {
        private readonly Dictionary<string, char> playerSymbols = new Dictionary<string, char>();
        private char[,] gameBoard = new char[3, 3];
        private string currentPlayerId;
        private bool isGameOver;

        public char AddPlayer(string connectionId)
        {
            char playerSymbol = playerSymbols.Count == 0 ? 'X' : 'O';
            playerSymbols.Add(connectionId, playerSymbol);

            // Set the first added player as the current player
            if (playerSymbols.Count == 1)
            {
                currentPlayerId = connectionId;
            }

            return playerSymbol;
        }

        public int GetPlayerCount()
        {
            return playerSymbols.Count;
        }

        public string GetOtherPlayerId(string currentPlayerId)
        {
            return playerSymbols.Keys.FirstOrDefault(id => id != currentPlayerId);
        }

        public bool IsPlayerTurn(string currentPlayerId)
        {
            return currentPlayerId == this.currentPlayerId;
        }

        public bool MakeMove(int row, int col, char symbol)
        {
            if (gameBoard[row, col] == '\0' && !isGameOver)
            {
                gameBoard[row, col] = symbol;
                return true;
            }

            return false;
        }

        public bool IsBoardFull()
        {
            return gameBoard.Cast<char>().All(cell => cell != '\0');
        }

        public char CheckForWinner()
        {
            // Check rows, columns, and diagonals for a winner
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] != '\0' && gameBoard[i, 0] == gameBoard[i, 1] && gameBoard[i, 1] == gameBoard[i, 2])
                    return gameBoard[i, 0];

                if (gameBoard[0, i] != '\0' && gameBoard[0, i] == gameBoard[1, i] && gameBoard[1, i] == gameBoard[2, i])
                    return gameBoard[0, i];
            }

            if (gameBoard[0, 0] != '\0' && gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2])
                return gameBoard[0, 0];

            if (gameBoard[0, 2] != '\0' && gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0])
                return gameBoard[0, 2];

            return '\0'; // No winner yet
        }

        public void SwitchTurn()
        {
            currentPlayerId = GetOtherPlayerId(currentPlayerId);
        }

        public void ResetGame()
        {
            gameBoard = new char[3, 3];
            isGameOver = false;
        }
    }
}
