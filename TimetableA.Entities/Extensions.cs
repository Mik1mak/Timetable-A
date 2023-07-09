using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.EntityFramework.Data;
using TimetableA.DataAccessLayer.EntityFramework.Repositories;
using TimetableA.DataAccessLayer.Repositories.Abstract;
namespace TimetableA.DataAccessLayer.EntityFramework;

public static class Extensions
{
    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITimetableRepository, TimetableRepository>();
        services.AddScoped<IGroupsRepository, GroupsRepository>();
        services.AddScoped<ILessonsRepository, LessonsRepository>();

        return services;
    }

    public static IServiceCollection UseInMemoryEntityFrameworkAccessLayer(this IServiceCollection services)
    {
        services.AddDbContext<TimetableAContext>(options =>
             options.UseInMemoryDatabase("Timetable"));

        return services.RegisterRepositories();
    }

    public static IServiceCollection UseEntityFrameworkAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TimetableAContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DevConnection"),
             b => b.MigrationsAssembly(typeof(TimetableAContext).Assembly.FullName)));

        return services.RegisterRepositories();
    }
}
