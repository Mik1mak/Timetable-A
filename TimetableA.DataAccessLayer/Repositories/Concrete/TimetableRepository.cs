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
using TimetableA.DataAccessLayer.Helpers;

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

                if (timetable.Id == default)
                {
                    timetable.ReadKey = KeyGen.Generate();
                    timetable.EditKey = KeyGen.Generate();
                    context.Entry(timetable).State = EntityState.Added;
                }
                else
                {
                    context.Entry(timetable).State = EntityState.Modified;
                }
                
                
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                return false;
            }
            
            return true;
        }

        public async Task<IEnumerable<Timetable>> GetAllBasicInfoAsync()
        {
            return (await context.Timetables.ToListAsync()).Select(x =>
            {
                x.ReadKey = x.EditKey = default;
                x.Gropus?.Clear();
                return x;
            });
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
                logger?.LogError(ex, ex.Message);
                return false;
            }

            return true;
        }
    }
}
