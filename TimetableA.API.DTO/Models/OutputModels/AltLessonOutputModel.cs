using System;
using TimetableA.Models;

namespace TimetableA.API.DTO.OutputModels
{
    public class AltLessonOutputModel : LessonOutputModel
    {
        public AltLessonOutputModel(Lesson lesson)
        {
            this.Id = lesson.Id;
            this.Name = lesson.Name;
            this.Start = lesson.Start;
            this.Link = lesson.Link;
            this.GroupId = lesson.GroupId;
            this.Duration = (int)lesson.Duration.TotalMinutes;
            this.Classroom = lesson.Classroom;
        }

        public string HexColor { get; set; }
    }
}
