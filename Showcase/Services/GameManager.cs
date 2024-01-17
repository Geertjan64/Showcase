// GameManager.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {
        private readonly Dictionary<string, char> playerSymbols = new Dictionary<string, char>();


        public char AddPlayer(string connectionId)
        {
            char playerSymbol = playerSymbols.Count == 0 ? 'X' : 'O';
            playerSymbols.Add(connectionId, playerSymbol);
            return playerSymbol;
        }

        public int GetPlayerCount()
        {
            return playerSymbols.Count;
        }

        // ... (other existing code)
    }
}
