using System;
using TimetableA.Models;

namespace TimetableA.API.DTO.OutputModels
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

        public AuthenticateResponse() { }

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
