﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int MaxCyclesPerTimetable { get; set; }
        public int MaxGroupsPerTimetable { get; set; }
    }
}
