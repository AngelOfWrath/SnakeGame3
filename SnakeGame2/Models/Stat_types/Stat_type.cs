using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGame2.Models.Stat_types
{
    public class Stat_type: IValidatableObject
    {
        public long Id { get; set; }
        [Required]
        public string Stat_Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Stat_Name.Contains(' ')) {
                yield return new ValidationResult(
                    $"name mustn't contain whitespaces",
                    new[] {"Stat_Name"});
            }
        }
    }
}
