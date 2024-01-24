using Showcase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {
        public TicTacToe Game;

        public GameManager() 
        { 

        }

        public void CreateGame(Player player)
        {
            Game = new TicTacToe(player);
        }

        public void JoinGame(string gameId, Player player) 
        {
            Game.JoinGame(gameId, player);
        }

        public void MakeMove(int row, int col, Player player)
        {
            Game.MakeMove(row, col, player);
        }
    }
}
