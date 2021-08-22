using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
{
    public class SimpleTimetableOutputModel
    {
        public ICollection<WeekOutputModel> Weeks { get; set; } = new List<WeekOutputModel>();

        public SimpleTimetableOutputModel(IEnumerable<Group> groups, AppSettings settings)
        {
            for (int i = 1; i <= settings.MaxWeeksInTimetable; i++)
            {
                var week = WeekOutputModel.GetWeekFromGroups(groups, i);

                if (week.Days.Any())
                    Weeks.Add(week);
            }
        }
    }
}
