using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimetableA.Models;
using TimetableA.DataAccessLayer.EntityFramework.Data;

namespace TimetableA.DataAccessLayer.EntityFramework.Repositories;

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

    public async virtual Task<TEntity> GetAsync(string id)
    {
        return await context.FindAsync<TEntity>(id);
    }

    public async virtual Task<bool> DeleteAsync(string id)
    {
        var entityToRemove = await context.FindAsync<TEntity>(id);

        if (entityToRemove == default)
            return false;

        try
        {
            context.Remove(entityToRemove);
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
