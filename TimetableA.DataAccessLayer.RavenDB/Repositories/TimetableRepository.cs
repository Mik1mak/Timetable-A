using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Models;
using TimetableA.DataAccessLayer.RavenDB.StoreHolder;
using AutoMapper;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using Microsoft.Extensions.Logging;
using System.Collections;
using Raven.Client.Documents;

namespace TimetableA.DataAccessLayer.RavenDB.Repositories;

internal class TimetableRepository : ITimetableRepository
{
    private readonly IMapper mapper;
    private readonly DocumentStoreHolder documentStoreHolder;

    protected IDocumentStore Store => documentStoreHolder.Store;

    protected static string MapNumberIdToString(int id) => $"timetables/{id}";


    public TimetableRepository(DocumentStoreHolder documentStoreHolder, IMapper mapper)
    {
        this.documentStoreHolder = documentStoreHolder;
        this.mapper = mapper;
    }

    public ILogger? Logger { protected get;  set; }

    public async Task<bool> DeleteAsync(int id)
    {
        string documentId = MapNumberIdToString(id);
        using(var session = Store.OpenAsyncSession())
        {
            Timetable? toDelete = await session.LoadAsync<Timetable?>(documentId);

            if (toDelete != null)
            {
                session.Delete(documentId);
                await session.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public Task<IEnumerable<Timetable>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Timetable> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAsync(Timetable model)
    {
        throw new NotImplementedException();
    }
}
