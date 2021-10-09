using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.DTO.OutputModels;
using TimetableA.Models;

namespace TimetableA.API.Helpers
{
    public class AltTimetableOutputModelFactory
    {
        public static AltTimetableOutputModel FromGroups(IEnumerable<Group> groups, AppSettings settings)
        {
            var output = new AltTimetableOutputModel()
            {
                Weeks = new List<WeekOutputModel>()
            };

            for (int i = 1; i <= settings.MaxCyclesPerTimetable; i++)
            {
                WeekOutputModel week = WeekOutputModelFactory.FromGroups(groups, i);

                if (week.Days.Any())
                    output.Weeks.Add(week);
            }

            return output;
        }
    }
}
