using System.Threading.Tasks;
using TimetableA.Models;

namespace TimetableA.Importer
{
    public interface ITimetableParser
    {
        Task<Timetable> GetTimetable();
    }
}
