using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Models.InputModels;
using TimetableA.API.Models.OutputModels;
using TimetableA.Entities.Models;

namespace TimetableA.API.Helpers
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            CreateMap<TimetableInputModel, Timetable>();
            CreateMap<Timetable, TimetableOutputModel>();
        }
    }
}
