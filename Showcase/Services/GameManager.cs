using Showcase.Areas.Identity.Data;
using Showcase.Models;

namespace Showcase.Services
{
    public class GameManager
    {
        private string _currentPlayer;

        private ShowcaseUser _player1;
        private ShowcaseUser _player2;

        public GameManager()
        {
            _currentPlayer = "X";
        }

        public string GetPlayer1()
        {
            return _player1.FirstName;
        }

        public string GetPlayer2() { 
            return _player2.FirstName;
        }

        public string GetCurrentPlayer()
        {
            return _currentPlayer;
        }
    }
}
