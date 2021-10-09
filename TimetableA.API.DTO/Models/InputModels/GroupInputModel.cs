using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.DTO.InputModels
{
    public class GroupInputModel
    {
        [Required]
        [MaxLength(32)]
        [MinLength(1)]
        public string Name { get; set; }

        [StringLength(7)]
        public string HexColor { get; set; }

    }
}
