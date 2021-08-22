using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.Models.OutputModels
{
    public class LessonOutputModel
    {
        public LessonOutputModel() { }

        public LessonOutputModel(TimeSpan duration)
        {
            Duration = (int)duration.TotalMinutes;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public int Duration { get; set; }

        public string Classroom { get; set; }

        public string Link { get; set; }

        public int GroupId { get; set; }
    }
}
