using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
{
    public class AuthenticateResponse : TimetableOutputModel
    {
        public AuthenticateResponse(Timetable model, string token)
        {
            this.Id = model.Id;
            this.Key = model.EditKey ?? model.ReadKey;
            this.Name = model.Name;
            this.Gropus = model.Gropus;
            this.Cycles = model.Cycles;
            this.CreateDate = model.CreateDate;
            Token = token;
        }

        public string Token { get; set; }
    }
}
