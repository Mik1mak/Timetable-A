using AutoMapper;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using TimetableA.DataAccessLayer.RavenDB.StoreHolder;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.RavenDB;

public abstract class BaseRepository<T> : IRepository<T>  where T : class, IModel
{
    protected readonly string collectionName;
    protected readonly IMapper mapper;

    private readonly DocumentStoreHolder documentStoreHolder;
    internal IDocumentStore DocumentStore => documentStoreHolder.Store;

    public ILogger? Logger { protected get; set; }

    protected BaseRepository(string validCollectionName, DocumentStoreHolder documentStoreHolder, IMapper mapper)
    {
        this.collectionName = validCollectionName;
        this.documentStoreHolder = documentStoreHolder;
        this.mapper = mapper;
    }

    public virtual async Task<bool> SaveAsync(T model)
    {
        using (IAsyncDocumentSession session = DocumentStore.OpenAsyncSession())
        {
            T? toUpdate = await session.LoadAsync<T>(model.Id);

            if (toUpdate == default)
                await session.StoreAsync(model);
            else
                mapper.Map<T, T>(model, toUpdate);
                

            await session.SaveChangesAsync();

            return true;
        }
    }

    public virtual async Task<T?> GetAsync(string id)
    {
        if (id == default)
            return default;

        using (IAsyncDocumentSession session = DocumentStore.OpenAsyncSession())
        {
            return await session.LoadAsync<T>(id);
        }
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        if (id == default)
            return false;

        using (IAsyncDocumentSession session = DocumentStore.OpenAsyncSession())
        {
            session.Delete(id);
            await session.SaveChangesAsync();
            return true;
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using (IAsyncDocumentSession session = DocumentStore.OpenAsyncSession())
        {
            return await session.Query<T>().ToListAsync();
        }
    }
}
