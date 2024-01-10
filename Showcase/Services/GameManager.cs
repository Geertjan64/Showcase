using System.Collections.Generic;
using System.Linq;

namespace Showcase.Services
{
    public class GameManager
    {
        private string _currentPlayer;
        private const string PlayerX = "X";
        private const string PlayerO = "O";

        private readonly Dictionary<string, string> _playerConnections = new Dictionary<string, string>();

        public GameManager()
        {
            _currentPlayer = PlayerX;
        }

        public string GetPlayer(string connectionId)
        {
            if (_playerConnections.ContainsKey(connectionId))
            {
                return _playerConnections[connectionId];
            }
            return null;
        }

        public string AssignPlayer(string connectionId)
        {
            string playerName = _playerConnections.Count == 0 ? "X" : "O";
            _playerConnections.Add(connectionId, playerName);
            return playerName;
        }

        public string GetOtherPlayerConnectionId(string connectionId)
        {
            var otherPlayer = _playerConnections.FirstOrDefault(x => x.Key != connectionId);
            return otherPlayer.Value;
        }

        public string GetPlayer1()
        {
            return PlayerX;
        }

        public string GetPlayer2()
        {
            return PlayerO;
        }

        public string GetNextPlayer()
        {
            return _currentPlayer;
        }

        public void SwitchPlayer()
        {
            _currentPlayer = (_currentPlayer == PlayerX) ? PlayerO : PlayerX;
        }
    }
}
