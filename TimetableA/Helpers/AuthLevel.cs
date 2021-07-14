using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableA.API.Helpers
{
    public enum AuthLevel
    {
        Unauthorized = 0,
        Read = 1,
        Edit = 2,
        Admin = 4,
    }
}
