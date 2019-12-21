using Microsoft.AspNetCore.Mvc;
using Rebuild.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rebuild.Models.Accounts
{
    public class Account
    {
        public long Id { get; set; }
        [Required]
        [Remote(action: "VerifyLogin",controller: "Accounts")]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
