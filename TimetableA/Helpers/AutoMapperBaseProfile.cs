using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;
using TimetableA.Models;

namespace TimetableA.API.Helpers
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            CreateMap<LessonInputModel, Lesson>();
            CreateMap<GroupInputModel, Group>();
            CreateMap<TimetableInputModel, Timetable>();

            CreateMap<Lesson, LessonOutputModel>();
            CreateMap<Group, GroupOutputModel>();
            CreateMap<Timetable, TimetableOutputModel>();
        }
    }
}
