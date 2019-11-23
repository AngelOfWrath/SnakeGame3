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
        [Required]

        public long Match_Id { get; set; }
        [Required]

        public long User_Id { get; set; }
        [Required]

        public float value { get; set; }

    }
}
