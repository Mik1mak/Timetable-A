using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
{
    public class DayOutputModel
    {
        public int DayOfWeek { get; set; }

        public IEnumerable<SimpleLessonOutputModel> Lessons { get; set; }

        public DayOutputModel(int dayOfWeek, IEnumerable<SimpleLessonOutputModel> lessons)
        {
            DayOfWeek = dayOfWeek;
            Lessons = lessons.OrderBy(l => l.Start).ToList();
        }
    }
}
