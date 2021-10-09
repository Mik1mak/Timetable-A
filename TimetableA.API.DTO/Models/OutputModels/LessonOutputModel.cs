using System;

namespace TimetableA.API.DTO.OutputModels
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
