using System;
using System.Linq;
using TimetableA.Models;

namespace TimetableA.ConsoleImporter
{
    public static class Extensions
    {
        public static Timetable TrimLessonsToCycles(this Timetable timetable)
        {
            DateTime maxDate = DateTime.MinValue + TimeSpan.FromDays(timetable.Cycles * 7);

            foreach (var group in timetable.Groups)
                group.Lessons = group.Lessons.Where(l => l.Start < maxDate).ToList();

            return timetable;
        }

        public static string SpliceIfTooLong(this string str, int maxLength)
        {
            if (str.Length > maxLength)
                str = str.Substring(0, maxLength);

            return str;
        }
    }
}
