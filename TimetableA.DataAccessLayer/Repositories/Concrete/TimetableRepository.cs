using Microsoft.EntityFrameworkCore;
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
    public class TimetableRepository : BaseRepository, ITimetableRepository
    {
        public TimetableRepository(TimetableAContext context) : base(context, null) { }

        public async Task<bool> SaveAsync(Timetable timetable)
        {
            try
            {
                if (timetable == null)
                    throw new ArgumentNullException(nameof(timetable));

                context.Entry(timetable).State = timetable.Id == default ? EntityState.Added : EntityState.Modified;
                
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Logger?.LogError(ex, ex.Message);
                return false;
            }
            
            return true;
        }

        public async Task<IEnumerable<Timetable>> GetAllAsync()
        {
            return await context.Timetables.ToListAsync();
        }

        public async Task<Timetable> GetAsync(int id)
        {
            return await context.Timetables.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var timetableToRemove = await context.Timetables.FindAsync(id);

            if (timetableToRemove == null)
                return false;

            try
            {
                context.Timetables.Remove(timetableToRemove);  
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, ex.Message);
                return false;
            }

            return true;
        }
    }
}
