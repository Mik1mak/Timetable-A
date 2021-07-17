using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Entities.Data;
using TimetableA.Entities.Models;

namespace TimetableA.DataAccessLayer.Repositories.Concrete
{
    public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
    {
        public GroupsRepository(TimetableAContext context) : base(context, null) { }
    }
}
