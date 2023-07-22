using AutoMapper;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TimetableA.DataAccessLayer.RavenDB.StoreHolder;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.RavenDB.Repositories;

public class LessonsRepository : ILessonsRepository
{
    private readonly IMapper mapper;
    private readonly DocumentStoreHolder documentStoreHolder;

    internal IDocumentStore DocumentStore => documentStoreHolder.Store;

    public ILogger? Logger { set; internal get; }

    public LessonsRepository(DocumentStoreHolder documentStoreHolder, IMapper mapper)
    {
        this.documentStoreHolder = documentStoreHolder;
        this.mapper = mapper;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        using (IAsyncDocumentSession sesion = DocumentStore.OpenAsyncSession())
        {
            Timetable? timetableWithLesson = await sesion.Query<Timetable>()
                .FirstOrDefaultAsync(t => t.Groups.Any(g => g.Lessons.Any(l => l.Id == id)));

            if (timetableWithLesson == null)
                return false;

            Lesson lessonToDel = timetableWithLesson.Groups.SelectMany(g => g.Lessons).First(l => l.Id == id);

            Group group = timetableWithLesson.Groups.First(g => g.Id == lessonToDel.GroupId);

            group.Lessons.Remove(lessonToDel);
            
            await sesion.SaveChangesAsync();
            return true;
        }
    }

    public Task<IEnumerable<Lesson>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Lesson?> GetAsync(string id)
    {
        using (IAsyncDocumentSession sesion = DocumentStore.OpenAsyncSession())
        {
            Timetable? timetableWithLesson = await sesion.Query<Timetable>()
                .FirstOrDefaultAsync(t => t.Groups.Any(g => g.Lessons.Any(l => l.Id == id)));

            if (timetableWithLesson == null)
                return null;

            return timetableWithLesson.Groups.SelectMany(g => g.Lessons).First(l => l.Id == id);
        }
    }

    public async Task<bool> SaveAsync(Lesson model)
    {
        using (IAsyncDocumentSession sesion = DocumentStore.OpenAsyncSession())
        {
            Timetable? timetable = await sesion.Query<Timetable>()
                .FirstOrDefaultAsync(t => t.Groups.Any(g => g.Id == model.GroupId));

            if (timetable == null)
                return false;

            Group group = timetable.Groups.First(group => group.Id == model.GroupId);

            if (model.Id == default)
            {
                model.Id = Guid.NewGuid().ToString();
                group.Lessons.Add(model);
            }
            else
            {
                Lesson? toUpdate = group.Lessons.First(l => l.Id == model.Id);

                if (toUpdate == null)
                    return false;

                mapper.Map<Lesson, Lesson>(model, toUpdate);
            }

            await sesion.SaveChangesAsync();
            return true;
        }
    }
}
