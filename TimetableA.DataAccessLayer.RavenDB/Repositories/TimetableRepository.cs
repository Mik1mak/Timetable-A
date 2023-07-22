using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.DataAccessLayer.RavenDB.StoreHolder;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.RavenDB.Repositories;

public class TimetableRepository : BaseRepository<Timetable>, ITimetableRepository
{
    public TimetableRepository(DocumentStoreHolder documentStoreHolder, IMapper mapper) 
        : base("Timetables", documentStoreHolder, mapper)
    {
    }
}
