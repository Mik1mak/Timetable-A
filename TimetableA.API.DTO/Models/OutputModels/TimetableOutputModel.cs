using System;
using System.Collections.Generic;

namespace TimetableA.API.DTO.OutputModels
{
    public class TimetableOutputModel
    {
        public string Name { get; set; }

        public int Cycles { get; set; }

        public bool DisplayEmptyDays { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<GroupOutputModel> Groups { get; set; }
    }
}
