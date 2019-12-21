using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rebuild.Models.Accounts;
using Rebuild.Models.Matches;
using Rebuild.Models.Mathes_stats;
using Rebuild.Models.Records;
using Rebuild.Models.Stat_types;
using Rebuild.Models.Users;

namespace Rebuild.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Match_stats> Match_stats { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<Stat_type> Stat_types { get; set; }
        public DbSet<User> Users1 { get; set; }

        public bool VerifyLogin(string login)
        {
            if (Accounts.Where(a => a.Login.Equals(login)).Count() == 0)
            {
                return true;
            }
            return false;
        }
    }
}
