using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.Models.InputModels
{
    public class LessonVerifyRequest
    {
        public LessonInputModel Lesson { get; set; }
        public IEnumerable<int> GroupIds { get; set; }
    }
}
