using System.ComponentModel.DataAnnotations;

namespace Showcase.Models
{
    public class Player
    {
        [Key]
        public string Id { get; set; }
        public int Symbol { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }

        public Player(string id, string firstName, string lastName, string mail)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Mail = mail;
        }
    }
}
