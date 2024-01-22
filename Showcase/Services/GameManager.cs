using System;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {
        private readonly Dictionary<string, char> playerSymbols = new Dictionary<string, char>();
        private string currentPlayerId;
        

        public char AddPlayer(string connectionId)
        {
            char playerSymbol = playerSymbols.Count == 0 ? 'X' : 'O';
            playerSymbols.Add(connectionId, playerSymbol);

            // Stel de eerste toegevoegde speler in als de huidige speler
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

    }
}
