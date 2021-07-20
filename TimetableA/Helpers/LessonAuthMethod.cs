using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Helpers
{
    public class LessonAuthMethod : IAuthValidationMethod
    {
        public bool Valid(string id, Timetable timetable)
        {   
            if (timetable.Gropus == null)
                return false;

            foreach (var group in timetable.Gropus)
            {
                if (group.Lessons != null)
                    if (group.Lessons.Any(x => x.Id.ToString() == id))
                        return true;
            }

            return false;
        }
    }
}
