using Showcase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {
        private readonly Dictionary<string, char> playerSymbols = new Dictionary<string, char>();
        private string currentPlayerId;
        private TicTacToe _ticTacToe;
        public GameManager() {
            //_ticTacToe = new TicTacToe();
        }

        public char AddPlayer(string connectionId)
        {
            char playerSymbol = playerSymbols.Count == 0 ? 'X' : 'O';
            playerSymbols.Add(connectionId, playerSymbol);

            if (playerSymbols.Count == 1)
            {
                currentPlayerId = connectionId;
            }

            return playerSymbol;
        }

        public void MakeMove(string playerId, int row, int col)
        {

        }

        public int GetPlayerCount()
        {
            return playerSymbols.Count;
        }

    }
}
