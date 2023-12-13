namespace Showcase.Services
{
    public class GameManager
    {
        private string _currentPlayer;
        public GameManager()
        {
            _currentPlayer = "X";
        }

        public string GetCurrentPlayer()
        {
            return _currentPlayer;
        }
    }
}
