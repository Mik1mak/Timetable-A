using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableA.DataAccessLayer.RavenDB.Models;

internal class Timetable : IDocument
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? ReadKey { get; set; }

    public string? EditKey { get; set; }

    public int Cycles { get; set; }

    public bool DisplayEmptyDays { get; set; }

    public DateTime CreateDate { get; set; }

    public ICollection<Group> Groups { get; set; } = new List<Group>();
}
