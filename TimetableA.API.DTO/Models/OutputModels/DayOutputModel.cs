using System;
using System.Collections.Generic;
using System.Linq;

namespace TimetableA.API.DTO.OutputModels
{
    public class DayOutputModel
    {
        public int DayOfWeek { get; set; }

        public IEnumerable<AltLessonOutputModel> Lessons { get; set; }

        public DayOutputModel(int dayOfWeek, IEnumerable<AltLessonOutputModel> lessons)
        {
            DayOfWeek = dayOfWeek;
            Lessons = lessons.OrderBy(l => l.Start).ToList();
        }
    }
}
