namespace Showcase.Models
{
    public class GameResultRecord
    {
        public int Id { get; set; }
        public string GameId { get; set; }
        public string Player1Id { get; set; }
        public string Player2Id { get; set; }
        public GameResult Result { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}
