using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.DTO.InputModels
{
    public class LessonInputModel : IValidatableObject
    {
        [Required]
        [MaxLength(32)]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        [Range(1, 1440)]
        public int Duration { get; set; }

        [MaxLength(32)]
        public string Classroom { get; set; }

        [MaxLength(512)]
        public string Link { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if((Start + TimeSpan.FromMinutes(Duration)).Date != Start.Date)
                yield return new ValidationResult($"End of lesson must be in this same day", new[] { nameof(Start), nameof(Duration) });
        }
    }
}
