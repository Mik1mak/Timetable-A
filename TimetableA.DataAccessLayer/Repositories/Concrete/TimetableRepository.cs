﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Entities.Data;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Entities.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TimetableA.DataAccessLayer.Repositories.Concrete
{
    public class TimetableRepository : BaseRepository<Timetable>, ITimetableRepository
    {
        public TimetableRepository(TimetableAContext context) : base(context, null) { }

    }
}
