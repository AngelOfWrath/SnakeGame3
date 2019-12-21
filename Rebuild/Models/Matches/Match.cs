﻿using Rebuild.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rebuild.Models.Matches
{
    public class Match
    {
        public long Id { get; set; }
        [Display(Name = "Users")]
        [MatchAttribute(2)]
        public List<User> Players { get; set; }
    }
}
