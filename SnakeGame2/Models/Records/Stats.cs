using SnakeGame2.Models.Matches;
using SnakeGame2.Models.Stat_types;
using SnakeGame2.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGame2.Models.Records
{
    public class Stats
    {
        public long Id { get; set; }
        [Required]
        public long Score { get; set; }
        public User User { get; set; }
        public Match Match { get; set; }
        public Stat_type Stat { get; set; }
    }
}
