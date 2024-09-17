namespace Showcase.Models
{
    public class UserWithGamesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<GameResultRecord> PlayedGames { get; set; }
    }
}