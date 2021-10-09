using System;
using System.Linq;
using TimetableA.Models;

namespace TimetableA.API.Helpers
{
    public class LessonAuthMethod : IAuthValidationMethod
    {
        public bool Valid(string id, Timetable timetable)
        {   
            if (timetable.Groups == null)
                return false;

            foreach (Group group in timetable.Groups)
            {
                if (group.Lessons != null)
                    if (group.Lessons.Any(x => x.Id.ToString() == id))
                        return true;
            }

            return false;
        }
    }
}
