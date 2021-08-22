using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int MaxWeeksInTimetable { get; set; }
        public int MaxCountOfGroups { get; set; }
    }
}
