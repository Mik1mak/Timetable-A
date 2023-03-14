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
            typeof(Stream),
            typeof(MemoryStream),
        };

        public async Task<Timetable> GetTimetable()
        {
            if (str == null)
                throw new InvalidOperationException();


            using StreamReader reader = new(str);
            Timetable timetable = await new IcsParser(reader).GetTimetable();

            return timetable;
        }

        public ITimetableFactory SetSource(object stream)
        {
            Stream? tmp = stream as Stream;

            if (tmp == null)
                throw new ArgumentException($"Invalid Type of source - {stream.GetType()}");

            str = tmp;
            return this;
        }

        public ITimetableFactory SetSource(ITimetableEndpoints api)
        {
            throw new NotSupportedException();
        }
    }
}
