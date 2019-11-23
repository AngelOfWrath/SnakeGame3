using SnakeGame2.Models.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGame2.Models.Users
{
    public class User
    {
        [Display(Name = "User Id")]
        public long Id { get; set; }
        [Display(Name = "Nickname")]
        [Required]
        public long User_Nickname { get; set; }
        public Account Account { get; set; }
    }
}
