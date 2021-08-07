using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Controllers
{
    public abstract class ControllerMyBase : ControllerBase
    {
        protected Timetable ThisTimetable { get => (Timetable)ControllerContext.HttpContext.Items["Timetable"]; }
    }
}
