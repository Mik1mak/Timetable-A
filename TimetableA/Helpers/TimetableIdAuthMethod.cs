using System;
using TimetableA.Models;

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
