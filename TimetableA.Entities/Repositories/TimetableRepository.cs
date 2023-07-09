using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.EntityFramework.Data;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.EntityFramework.Repositories;

public class TimetableRepository : BaseRepository<Timetable>, ITimetableRepository
{
    public TimetableRepository(TimetableAContext context) : base(context, null) { }

    public async override Task<IEnumerable<Timetable>> GetAllAsync()
    {
        return await context.Set<Timetable>()
            .Include(x => x.Groups)
            .ThenInclude(x => x.Lessons)
            .ToListAsync();
    }

    public async override Task<Timetable> GetAsync(int id)
    {
        return await context.Set<Timetable>()
            .Include(x => x.Groups)
            .ThenInclude(x => x.Lessons)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
