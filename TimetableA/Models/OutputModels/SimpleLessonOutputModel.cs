using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
{
    public class SimpleLessonOutputModel : LessonOutputModel
    {
        public SimpleLessonOutputModel(Lesson lesson)
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
