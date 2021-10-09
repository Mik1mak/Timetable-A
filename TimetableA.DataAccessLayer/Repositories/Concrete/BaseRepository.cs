using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Entities.Data;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.Repositories.Concrete
{
    public abstract class BaseRepository<TEntity> where TEntity : class, IModel
    {
        protected readonly TimetableAContext context;
        public ILogger Logger { protected get; set; }

        public BaseRepository(TimetableAContext context, ILogger logger)
        {
            this.context = context;
            Logger = logger;
        }

        public async virtual Task<bool> SaveAsync(TEntity entity)
        {
            try
            {
                if (entity == default)
                    throw new ArgumentNullException(nameof(entity));

                context.Entry(entity).State = entity.Id == default ? EntityState.Added : EntityState.Modified;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, ex.Message);
                return false;
            }

            return true;
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async virtual Task<TEntity> GetAsync(int id)
        {
            return await context.FindAsync<TEntity>(id);
        }

        public async virtual Task<bool> DeleteAsync(int id)
        {
            var entityToRemove = await context.FindAsync<TEntity>(id);

            if (entityToRemove == default)
                return false;

            try
            {
                context.Remove<TEntity>(entityToRemove);
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
