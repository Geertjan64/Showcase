using Showcase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {

        private readonly Dictionary<string, char> playerSymbols = new Dictionary<string, char>();
        private TicTacToe _ticTacToe;

        public GameManager() 
        { 

        }

        public void CreateGame(Player player)
        {
            _ticTacToe = new TicTacToe(player);
        }

        public void JoinGame(Player player) 
        {
            _ticTacToe.JoinGame(player);
        }

        public void MakeMove(int row, int col, Player player)
        {
            _ticTacToe.MakeMove(row, col, player);
        }
    }
}
