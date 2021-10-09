using System;
using System.Collections.Generic;

namespace TimetableA.API.DTO.OutputModels
{
    public class AltTimetableOutputModel
    {
        public ICollection<WeekOutputModel> Weeks { get; set; } = new List<WeekOutputModel>();
    }
}
