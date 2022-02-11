using TimetableA.Importer;
using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    public class IcsSourceType : ITimetableSourceType
    {
        private Stream? str;

        public bool ParseFromUrl { get; init; } = true;

        public bool ParseFromFile { get; init; } = true;

        public string DisplayName { get; init; } = "ICS";

        public string NameOf => nameof(IcsSourceType);

        public bool AskAboutCycles { get; init; } = true;

        public IEnumerable<Type> AcceptedSources => new HashSet<Type>()
        {
            typeof(Stream)
        };

        public Task<Timetable> GetTimetable()
        {
            if (str == null)
                throw new InvalidOperationException();

            using StreamReader reader = new(str);
            Timetable timetable = new IcsParser(reader).GetTimetable();
            return Task.FromResult(timetable);
        }

        public ITimetableFactory SetSource(object stream)
        {
            if (!AcceptedSources.Contains(stream.GetType()))
                throw new ArgumentException("Invalid Type of source");

            str = stream as Stream;
            return this;
        }

        public ITimetableFactory SetSource(ITimetableEndpoints api)
        {
            throw new NotSupportedException();
        }
    }
}
