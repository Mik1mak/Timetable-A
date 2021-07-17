using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
{
    public class AuthenticateResponse : TimetableOutputModel
    {
        public AuthenticateResponse(Timetable model, string token, AuthLevel authLvl)
        {
            this.Id = model.Id;
            this.Key = model.EditKey ?? model.ReadKey;
            this.Name = model.Name;
            this.Gropus = model.Gropus?.Select(x => {
                return new GroupOutputModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    HexColor = x.HexColor,
                    Lessons = x.Lessons,
                };
            }).ToList();
            this.Cycles = model.Cycles;
            this.CreateDate = model.CreateDate;

            Token = token;
            this.authLevel = authLvl;
        }

        public string Token { get; set; }

        private readonly AuthLevel authLevel;
        public string AuthLevel { get => authLevel.GetStr(); } 
    }
}
