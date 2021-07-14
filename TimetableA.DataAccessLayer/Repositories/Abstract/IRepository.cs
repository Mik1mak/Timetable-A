using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableA.DataAccessLayer.Repositories.Abstract
{
    public interface IRepository
    {
        public ILogger Logger { set; }
    }
}
