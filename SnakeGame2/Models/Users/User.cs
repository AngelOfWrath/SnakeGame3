using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGame2.Models.Users
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        public long User_Nickname { get; set; }
        [Required]
        public long User_Type_Id { get; set; }
        [Required]
        public long Account_Id { get; set; }
    }
}
