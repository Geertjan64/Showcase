﻿using System.ComponentModel.DataAnnotations;

namespace Showcase.Models
{
    public class Player
    {
        [Key]
        public string Id { get; set; }
        public string Symbol { get; set; }

        public Player(string id)
        {
            Id = id;
        }
    }
}
