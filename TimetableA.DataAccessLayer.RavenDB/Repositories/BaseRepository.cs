using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.RavenDB.Models;
using TimetableA.DataAccessLayer.RavenDB.StoreHolder;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.RavenDB;

internal abstract class BaseRepository<TAPIModel, TDocument> : IRepository<TAPIModel>  where TAPIModel : class, IModel where TDocument : IDocument
{
    //private class ModelWrapper<TModel> where TModel : class, IModel
    //{
    //    private readonly string collectionName;

    //    public TModel Model { get; set; }

    //    internal ModelWrapper(TModel model, string collectionName) 
    //    {
    //        Model = model;
    //        this.collectionName = collectionName;
    //    }

    //    public string? Id 
    //    { 
    //        get
    //        {
    //            if(Model.Id == default)
    //                return null;
    //            return MakeStringId(collectionName, Model.Id);
    //        }
    //        set
    //        {
    //            if(value == null)
    //            {
    //                Model.Id = default;
    //            }
    //            else
    //            {
    //                string[] splitedValue = value.Split(new[] { '/', '-' });
    //                if (splitedValue[0] != collectionName)
    //                    throw new Exception("Invalid collection Name");
    //                Model.Id = int.Parse(splitedValue[1]);
    //            }
    //        }
    //    }

    //    public static string MakeStringId(string collectionName, int numericId) => $"{collectionName}/{numericId}";
    //}

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

    protected abstract Task<TDocument> MapToDocument(TAPIModel model, IAsyncDocumentSession session);
    protected abstract Task<TAPIModel> MapFromDocument(TDocument model, IAsyncDocumentSession session);

    protected string MapNumberIdToString(int id) => $"{collectionName}/{id}";
    protected int MapStringIdToNumber(string id)
    {
        string[] splitedValue = id.Split(new[] { '/', '-' });
        if (splitedValue[0] != collectionName)
            throw new Exception("Invalid collection Name");
        return int.Parse(splitedValue[1]);
    }

    public virtual async Task<bool> SaveAsync(TAPIModel model)
    {
        using (IAsyncDocumentSession session = DocumentStore.OpenAsyncSession())
        {
            TDocument document = await MapToDocument(model, session);
            TDocument? toUpdate = await session.LoadAsync<TDocument>(document.Id);

            if (toUpdate == null)
                await session.StoreAsync(document);
            else
                mapper.Map<TDocument, TDocument>(document, toUpdate);

            await session.SaveChangesAsync();

            return true;
        }
    }

    public virtual async Task<TAPIModel> GetAsync(int id)
    {
        using (IAsyncDocumentSession session = DocumentStore.OpenAsyncSession())
        {
            return await MapFromDocument(await session.LoadAsync<TDocument>(MapNumberIdToString(id)), session);
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        using (IAsyncDocumentSession session = DocumentStore.OpenAsyncSession())
        {
            session.Delete(MapNumberIdToString(id));
            await session.SaveChangesAsync();
            return true;
        }
    }

    public virtual async Task<IEnumerable<TAPIModel>> GetAllAsync()
    {
        using (IAsyncDocumentSession session = DocumentStore.OpenAsyncSession())
        {
            return (await session.Query<TDocument>().ToArrayAsync()).Select(async model => await MapFromDocument(model, session)).ToList();
        }
    }
}
