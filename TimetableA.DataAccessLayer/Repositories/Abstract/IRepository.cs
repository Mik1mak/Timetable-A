using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.Repositories.Abstract
{
    public interface IRepository<T> where T : class, IModel
    {
        public ILogger Logger { set; }

        public Task<bool> SaveAsync(T model);

        public Task<T> GetAsync(string id);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<bool> DeleteAsync(string id);
    }
}
