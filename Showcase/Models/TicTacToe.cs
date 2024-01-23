using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Razor;
using System.Composition.Convention;

namespace Showcase.Models
{
    public enum Turn {
        Player1 = 1,
        Player2 = 2,
    }

    public enum GameState
    {
        WaitingForSecondPlayer,
        Started,
        Finished
    }

    public enum GameResult
    {
        Draw,
        Player1,
        Player2
    }

    public class TicTacToe
    {
        public int Id { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int[,] Board {  get; set; }
        public Turn Turn {  get; set; }
        public GameState GameState { get; set; }
        public GameResult GameResult { get; set; }

        public TicTacToe(Player startingPlayer)
        {
            Board = new int[3, 3];
            Player1 = startingPlayer;
            GameState = GameState.WaitingForSecondPlayer;
        }

        public bool ValidateMove(int row, int col)
        {
            // Check if the move is in the bord and if the cell is empty
            if (row <= 2 && row >= 0 && col <= 2 && col >= 0)
            {
                if (Board[row, col] == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MakeMove(int row, int col)
        { 
            if (!ValidateMove(row, col)) { return false; }

            Board[row, col] = (int)Turn;
            CheckGameState();
            SwitchPlayer();

            return true;
        }

        public void SwitchPlayer()
        {
            if (Turn == Turn.Player1)
            {
                Turn = Turn.Player2;
            }

            else
            {
                Turn = Turn.Player1;
            }
        }

        public void JoinGame(Player player2)
        {
            Player2 = player2;
            GameState = GameState.Started;
        }

        public void CheckGameState()
        {
            // Check horizontal and vertical
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == Board[i, 1] && Board[i, 1] == Board[i, 2] && Board[i, 0] != 0)
                {
                    CheckWinner(Board[i, 0]);
                    return;
                }
                if (Board[0, i] == Board[1, i] && Board[1, i] == Board[2, i] && Board[0, i] != 0)
                {
                    CheckWinner(Board[i, 0]);
                    return;
                }
            }

            // Check diagonal
            if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2] && Board[0, 0] != 0)
            {
                CheckWinner(Board[0, 0]);
                return;
            }
            if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0] && Board[0, 2] != 0)
            {
                CheckWinner(Board[0, 2]);
                return;
            }

            // Controleer of het bord volledig is gevuld
            bool isBoardFull = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == 0)
                    {
                        isBoardFull = false;
                        break;
                    }
                }
            }
            if (isBoardFull)
            {
                GameResult = GameResult.Draw;
                GameState = GameState.Finished;
            }
        }

        public void CheckWinner(int player)
        {
            if (player == 1)
            {
                GameResult = GameResult.Player1;
                GameState = GameState.Finished;
            }
            
            if (player == 2) 
            {
                GameResult = GameResult.Player2;
                GameState = GameState.Finished;
            }
        }
    }
}
