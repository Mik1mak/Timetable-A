using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.DTO.InputModels
{
    public class LessonVerifyRequest
    {
        public LessonInputModel Lesson { get; set; }
        public IEnumerable<string> GroupIds { get; set; }
    }
}
