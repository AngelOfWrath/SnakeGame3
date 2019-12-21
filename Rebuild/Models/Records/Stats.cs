using Rebuild.Models.Matches;
using Rebuild.Models.Stat_types;
using Rebuild.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rebuild.Models.Records
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
