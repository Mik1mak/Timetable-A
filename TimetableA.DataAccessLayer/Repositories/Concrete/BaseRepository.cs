using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Entities.Data;
using TimetableA.DataAccessLayer.Repositories.Abstract;

namespace TimetableA.DataAccessLayer.Repositories.Concrete
{
    public abstract class BaseRepository
    {
        protected readonly TimetableAContext context;
        public ILogger Logger { protected get; set; }

        public BaseRepository(TimetableAContext context, ILogger logger)
        {
            this.context = context;
            Logger = logger;
        }
    }
}
