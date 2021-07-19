using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.Models.InputModels
{
    public class LessonInputModel
    {
        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Duration { get; set; }

        [MaxLength(32)]
        public string Classroom { get; set; }

        [MaxLength(512)]
        public string Link { get; set; }
    }
}
