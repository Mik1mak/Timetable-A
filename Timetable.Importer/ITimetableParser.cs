using TimetableA.Models;

namespace TimetableA.Importer
{
    public interface ITimetableParser
    {
        Timetable GetTimetable();
    }
}
