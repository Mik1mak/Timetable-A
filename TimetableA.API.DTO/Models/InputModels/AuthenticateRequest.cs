using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.DTO.InputModels
{
    public class AuthenticateRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Key { get; set; }
    }
}
