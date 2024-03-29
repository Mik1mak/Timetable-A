﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.RavenDB.Repositories;
using TimetableA.DataAccessLayer.RavenDB.StoreHolder;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoMapper;


namespace TimetableA.DataAccessLayer.RavenDB;

public static class Extensions
{
    public static IServiceCollection UseRavenDBDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        services.Configure<RavenDbSettings>(configuration.GetSection(RavenDbSettings.Position));

        services.AddSingleton<DocumentStoreHolder>();
        services.AddScoped<ITimetableRepository, TimetableRepository>();
        services.AddScoped<IGroupsRepository, GroupsRepository>();
        services.AddScoped<ILessonsRepository, LessonsRepository>();

        return services;
    }
}
