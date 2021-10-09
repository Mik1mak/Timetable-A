using System;
using System.Collections.Generic;

namespace TimetableA.API.DTO.OutputModels
{
    public class WeekOutputModel
    {
        public int Number { get; set; }

        public DateTime MinStart { get; set; }

        public DateTime MaxStop { get; set; }

        public int MinDuration { get; set; }

        public ICollection<DayOutputModel> Days { get; set; } = new List<DayOutputModel>();

    }
}
