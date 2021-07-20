using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Helpers
{
    public class GroupAuthMethod : IAuthValidationMethod
    {
        public bool Valid(string id, Timetable timetable)
        {
            if (timetable.Gropus == null)
                return false;

            if (timetable.Gropus.Any(x => x.Id.ToString() == id))
                return true;

            return false;
        }
    }
}
