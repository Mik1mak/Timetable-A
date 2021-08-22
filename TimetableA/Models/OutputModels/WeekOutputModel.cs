using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
{
    public class WeekOutputModel
    {
        public int Number { get; set; }

        public DateTime MinStart { get; set; }

        public DateTime MaxStop { get; set; }

        public int MinDuration { get; set; }

        public ICollection<DayOutputModel> Days { get; set; } = new List<DayOutputModel>();

        public static WeekOutputModel GetWeekFromGroups(IEnumerable<Group> groups, int weekIndex)
        {
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;

            var weekLessons = new List<SimpleLessonOutputModel>();

            foreach (var group in groups)
            {
                var singleGroupWeeksLessons = group.Lessons.Where(l => calendar.GetWeekOfYear(l.Start,
                    CalendarWeekRule.FirstDay, DayOfWeek.Monday) == weekIndex)
                    .Select(l => new SimpleLessonOutputModel(l) { HexColor = group.HexColor });

                weekLessons.AddRange(singleGroupWeeksLessons);
            }

            return new WeekOutputModel(weekIndex, weekLessons);
        }

        private WeekOutputModel(int number, IEnumerable<SimpleLessonOutputModel> lessons)
        {
            Number = number;

            for (int day = 1; day <= 7; day++)
            {
                var dayOfWeek = (DayOfWeek)(day % 7);
                var dayLessons = lessons.Where(l => l.Start.DayOfWeek == dayOfWeek);

                if (dayLessons.Any())
                    Days.Add(new DayOutputModel(day, dayLessons));
            }

            if(Days.Any())
            {
                MinStart = DateTime.MinValue + Days.Min(d => d.Lessons.Min(l => l.Start.TimeOfDay));
                MaxStop = DateTime.MinValue + Days.Max(d => d.Lessons.Max(l => (l.Start + TimeSpan.FromMinutes(l.Duration)).TimeOfDay));
                MinDuration = Days.Min(d => d.Lessons.Min(l => l.Duration));
            }
        }
    }
}
