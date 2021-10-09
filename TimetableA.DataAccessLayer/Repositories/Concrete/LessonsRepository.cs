using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Entities.Data;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.Repositories.Concrete
{
    public class LessonsRepository : BaseRepository<Lesson>, ILessonsRepository
    {
        public LessonsRepository(TimetableAContext context) : base(context, null) { }
    }
}
