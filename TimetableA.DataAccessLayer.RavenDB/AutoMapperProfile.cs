using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.RavenDB;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Lesson, Lesson>();
        CreateMap<Group, Group>();
        CreateMap<Timetable, Timetable>();
    }
}