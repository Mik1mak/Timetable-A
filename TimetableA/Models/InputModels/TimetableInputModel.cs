using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.Models.InputModels
{
    public class TimetableInputModel
    {
        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        public string Name { get; set; }

        [Range(1, 64)]
        public int Cycles { get; set; }
    }
}
