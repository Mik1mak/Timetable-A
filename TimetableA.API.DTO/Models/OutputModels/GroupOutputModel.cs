using System;
using System.Collections.Generic;

namespace TimetableA.API.DTO.OutputModels
{
    public class GroupOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string HexColor { get; set; }

        public IEnumerable<LessonOutputModel> Lessons { get; set; }

        public IEnumerable<int> CollidingGroups { get; set; }
    }
}
