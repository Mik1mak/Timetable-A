﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.DataAccessLayer.Repositories.Abstract
{
    public interface ITimetableRepository
    {
        public Task<bool> SaveAsync(Timetable timetable);

        public Task<Timetable> GetAsync(int id);

        public Task<IEnumerable<Timetable>> GetAllBasicInfoAsync();

        public Task<bool> DeleteAsync(int id);
    }
}
