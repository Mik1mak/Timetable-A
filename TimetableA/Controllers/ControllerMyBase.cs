using Microsoft.AspNetCore.Mvc;
using System;
using TimetableA.Models;

namespace TimetableA.API.Controllers
{
    public abstract class ControllerMyBase : ControllerBase
    {
        protected Timetable ThisTimetable { get => (Timetable)ControllerContext.HttpContext.Items["Timetable"]; }
    }
}
