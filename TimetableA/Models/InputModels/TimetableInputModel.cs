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
        [MaxLength(32)]
        [MinLength(1)]
        public string Name { get; set; }

        public int Cycles { get; set; }

        public bool ShowWeekend { get; set; } = false;
    }
}
