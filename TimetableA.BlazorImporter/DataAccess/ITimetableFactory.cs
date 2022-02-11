using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    public interface ITimetableFactory
    {
        public ITimetableFactory SetSource(object source);
        public Task<Timetable> GetTimetable();
    }
}
