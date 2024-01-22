using System.ComponentModel.DataAnnotations;

namespace Showcase.Models
{
    public class Player
    {
        [Key]
        public string Id { get; set; }
        public char Symbol { get; set; }

        public Player(string id, char symbol)
        {
            Id = id;
            Symbol = symbol;
        }
    }
}
