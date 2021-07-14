using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Models.OutputModels
{
    public class TimetableOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Cycles { get; set; }

        public string Key { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<Group> Gropus { get; set; }
    }
}
