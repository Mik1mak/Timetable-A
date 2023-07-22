using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.EntityFramework.Data;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.EntityFramework.Repositories;

public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
{
    public GroupsRepository(TimetableAContext context) : base(context, null) { }

    public async override Task<IEnumerable<Group>> GetAllAsync()
    {
        return await context.Set<Group>()
            .Include(x => x.Lessons)
            .ToListAsync();
    }

    public async override Task<Group> GetAsync(string id)
    {
        return await context.Set<Group>()
            .Include(x => x.Lessons)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
