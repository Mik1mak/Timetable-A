using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(Timetable model, string token)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Cycles = model.Cycles;
            this.CreateDate = model.CreateDate;
            this.ReadKey = model.ReadKey;
            this.EditKey = model.EditKey;
            this.DisplayEmptyDays = model.DisplayEmptyDays;

            Token = token;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ReadKey { get; set; }

        public string EditKey { get; set; }

        public int Cycles { get; set; }

        public bool DisplayEmptyDays { get; set; }

        public DateTime CreateDate { get; set; }

        public string Token { get; set; }

        public int MaxGroupsPerTimetable { get; set; }
        public int MaxCyclesPerTimetable { get; set; }
    }
}
