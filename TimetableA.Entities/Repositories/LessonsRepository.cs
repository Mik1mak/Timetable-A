using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.EntityFramework.Data;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.EntityFramework.Repositories;

public class LessonsRepository : BaseRepository<Lesson>, ILessonsRepository
{
    public LessonsRepository(TimetableAContext context) : base(context, null) { }
}
