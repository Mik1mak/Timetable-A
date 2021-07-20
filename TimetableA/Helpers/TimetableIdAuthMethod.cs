using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Helpers
{
    public class TimetableIdAuthMethod : IAuthValidationMethod
    {
        public bool Valid(string id, Timetable timetable)
        {
            if (timetable.Id.ToString() == id)
                return true;
            return false;
        }
    }
}
