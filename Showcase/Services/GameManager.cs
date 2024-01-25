﻿using MimeKit.Cryptography;
using Showcase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {
        public TicTacToe Game;

        public GameManager() { }

        public Player GetPlayer(string playerId)
        {
            if (Game.Player1.Id == playerId)
            {
                return Game.Player1;
            }
            else if (Game.Player2.Id == playerId)
            {
                return Game.Player2;
            }
            else
            {
                throw new Exception("Player not found");
            }
        }

        public Player ReturnOpponent(string playerId)
        {
            if (Game.Player1.Id == playerId)
            {
                return Game.Player2;
            }

            return Game.Player1;
        }

        public void CreateGame(Player player)
        {
            Game = new TicTacToe(player);
        }

        public void JoinGame(string gameId, Player player)
        {
            Game.JoinGame(gameId, player);
        }

        public bool MakeMove(int row, int col, Player player)
        {
            return Game.MakeMove(row, col, player);
        }

        public bool CheckIfGameIsOver()
        {
            if (Game.GameState == GameState.Finished) return true;

            return false;
        }

        public GameResult GetGameResult()
        {
            return Game.GameResult;
        }
        
    }
}
