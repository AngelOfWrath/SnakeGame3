using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGame2.Models.Stat_types
{
    public class Stat_type
    {
        public long Id { get; set; }
        [Required]
        public long Stat_Name { get; set; }
    }
}
