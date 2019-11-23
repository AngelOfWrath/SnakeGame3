using Microsoft.EntityFrameworkCore;
using SnakeGame2.Models.Accounts;
using SnakeGame2.Models.Matches;
using SnakeGame2.Models.Mathes_stats;
using SnakeGame2.Models.Records;
using SnakeGame2.Models.Stat_types;
using SnakeGame2.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGame2.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Match_stats> Match_stats { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<Stat_type> Stat_types { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
