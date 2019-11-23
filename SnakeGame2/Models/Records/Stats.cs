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
        [Required]
        public long User_Id { get; set; }
        [Required]
        public long Match_Id { get; set; }
        [Required]
        public long Stat_Id { get; set; }
    }
}
