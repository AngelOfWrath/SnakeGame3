using Rebuild.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rebuild.Models.Matches
{
    public class MatchAttribute:ValidationAttribute
    {
        private int users_count;

        public MatchAttribute(int users_count) {
            this.users_count = users_count;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Match = (Match)validationContext.ObjectInstance;
            var Users = (List<User>)value;
            if (Users != null)
            {
                if (Users.Count < users_count)
                {
                    return new ValidationResult(getErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        public int Users_count => users_count;

        public string getErrorMessage() {
            return $"Must be more than {users_count}";
        }
    }
}
