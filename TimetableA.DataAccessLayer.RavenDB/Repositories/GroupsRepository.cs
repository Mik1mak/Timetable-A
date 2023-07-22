using AutoMapper;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.RavenDB.StoreHolder;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.RavenDB.Repositories;

public class GroupsRepository : IGroupsRepository
{
    private readonly IMapper mapper;
    private readonly DocumentStoreHolder documentStoreHolder;
    
    internal IDocumentStore DocumentStore => documentStoreHolder.Store;

    public ILogger? Logger { set; internal get; }

    public GroupsRepository(DocumentStoreHolder documentStoreHolder, IMapper mapper)
    {
        this.documentStoreHolder = documentStoreHolder;
        this.mapper = mapper;
    }
    public async Task<bool> DeleteAsync(string id)
    {
        using(IAsyncDocumentSession sesion = DocumentStore.OpenAsyncSession())
        {
            Timetable? timetableWithGroupToDel = await sesion.Query<Timetable>()
                .FirstOrDefaultAsync(t => t.Groups.Any(g => g.Id == id));

            if(timetableWithGroupToDel == null)
                return false;

            Group toDel = timetableWithGroupToDel.Groups.First(g => g.Id == id);
            timetableWithGroupToDel.Groups.Remove(toDel);

            await sesion.SaveChangesAsync();
            return true;
        }
    }

    public Task<IEnumerable<Group>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Group?> GetAsync(string id)
    {
        using (IAsyncDocumentSession sesion = DocumentStore.OpenAsyncSession())
        {
            Timetable? timetableWithGroup = await sesion.Query<Timetable>()
                .FirstOrDefaultAsync(t => t.Groups.Any(g => g.Id == id));

            if (timetableWithGroup == null)
                return null;

            return timetableWithGroup.Groups.First(g => g.Id == id);
        }
    }

    public async Task<bool> SaveAsync(Group model)
    {
        using (IAsyncDocumentSession sesion = DocumentStore.OpenAsyncSession())
        {
            Timetable? timetable = await sesion.LoadAsync<Timetable?>(model.TimetableId);

            if (timetable == null)
                return false;

            if (model.Id == default) 
            { 
                model.Id = Guid.NewGuid().ToString();
                timetable.Groups.Add(model);
            }
            else
            {
                Group toUpdate = timetable.Groups.First(g => g.Id == model.Id);
                mapper.Map<Group, Group>(model, toUpdate);
            }

            await sesion.SaveChangesAsync();
            return true;
        }
    }
}
