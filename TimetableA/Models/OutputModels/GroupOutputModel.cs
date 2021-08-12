﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
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
