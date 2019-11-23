using SnakeGame2.Models.Matches;
using SnakeGame2.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGame2.Models.Mathes_stats
{
    public class Match_stats
    {
        public long Id { get; set; }

        public Match Match { get; set; }


        public User User { get; set; }
        [Required]

        public float value { get; set; }

    }
}
