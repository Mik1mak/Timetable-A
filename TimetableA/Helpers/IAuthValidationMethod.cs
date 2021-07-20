using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Helpers
{
    public interface IAuthValidationMethod
    {
        public bool Valid(string id, Timetable timetable);
    }
}
