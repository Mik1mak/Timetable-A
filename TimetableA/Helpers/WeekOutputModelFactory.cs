using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TimetableA.API.DTO.OutputModels;
using TimetableA.Models;

namespace TimetableA.API.Helpers
{
    public class WeekOutputModelFactory
    {
        public static WeekOutputModel FromGroups(IEnumerable<Group> groups, int weekIndex)
        {
            IEnumerable<AltLessonOutputModel> lessonsOfWeek = GetLessonsFromWeek(groups, weekIndex);

            var output = new WeekOutputModel()
            {
                Number = weekIndex,
                Days = new List<DayOutputModel>(),
            };

            AddDays(output, lessonsOfWeek);

            SetExtemes(output);

            return output;
        }

        private static List<AltLessonOutputModel> GetLessonsFromWeek(IEnumerable<Group> groups, int weekOfYear)
        {
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekLessons = new List<AltLessonOutputModel>();

            foreach (var group in groups)
            {
                var singleGroupWeeksLessons = group.Lessons.Where(l => calendar.GetWeekOfYear(l.Start,
                    CalendarWeekRule.FirstDay, DayOfWeek.Monday) == weekOfYear)
                    .Select(l => new AltLessonOutputModel(l) { HexColor = group.HexColor });

                weekLessons.AddRange(singleGroupWeeksLessons);
            }

            return weekLessons;
        }

        private static void AddDays(WeekOutputModel week, IEnumerable<AltLessonOutputModel> lessonOfWeek)
        {
            for (int day = 1; day <= 7; day++)
            {
                var dayOfWeek = (DayOfWeek)(day % 7);
                var dayLessons = lessonOfWeek.Where(l => l.Start.DayOfWeek == dayOfWeek);

                if (dayLessons.Any())
                    week.Days.Add(new DayOutputModel(day, dayLessons));
            }
        }

        private static void SetExtemes(WeekOutputModel week)
        {
            if (week.Days.Any())
            {
                week.MinStart = DateTime.MinValue + week.Days.Min(d => d.Lessons.Min(l => l.Start.TimeOfDay));
                week.MaxStop = DateTime.MinValue +
                    week.Days.Max(d => d.Lessons.Max(l => (l.Start + TimeSpan.FromMinutes(l.Duration)).TimeOfDay));
                week.MinDuration = week.Days.Min(d => d.Lessons.Min(l => l.Duration));
            }
        }
    }
}
