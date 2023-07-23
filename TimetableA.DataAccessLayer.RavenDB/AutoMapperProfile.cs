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
        CreateMap<Lesson, Lesson>()
            .ForMember(l => l.GroupId, opt => opt.Ignore())
            .ForMember(l => l.Group, opt => opt.Ignore())
            .ForMember(l => l.Id, opt => opt.Ignore());
        CreateMap<Group, Group>()
            .ForMember(g => g.Id, opt => opt.Ignore())
            .ForMember(g => g.TimetableId, opt => opt.Ignore())
            .ForMember(g => g.Timetable, opt => opt.Ignore())
            .ForMember(g => g.Lessons, opt => opt.Ignore());
        CreateMap<Timetable, Timetable>()
            .ForMember(t => t.Id, opt => opt.Ignore())
            .ForMember(t => t.ReadKey, opt => opt.Ignore())
            .ForMember(t => t.EditKey, opt => opt.Ignore())
            .ForMember(t => t.CreateDate, opt => opt.Ignore())
            .ForMember(t => t.Groups, opt => opt.Ignore());
    }
}