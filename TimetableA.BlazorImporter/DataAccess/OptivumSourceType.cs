using TimetableA.Importer;
using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    public class OptivumSourceType : ITimetableSourceType
    {
        private Stream? str;

        public bool ParseFromUrl { get; init; } = false;

        public bool ParseFromFile { get; init; } = true;

        public string DisplayName { get; init; } = "Optivum";

        public string NameOf => nameof(OptivumSourceType);

        public bool AskAboutCycles { get; init; } = false;

        public IEnumerable<Type> AcceptedSources => new HashSet<Type>()
        {
            typeof(Stream)
        };

        public async Task<Timetable> GetTimetable()
        {
            if (str == null)
                throw new InvalidOperationException();

            string htmlDoc;
            using (StreamReader docReader = new(str))
            {
                htmlDoc = await docReader.ReadToEndAsync();
            }

            return await new OptivumParser(htmlDoc).GetTimetable();
        }

        public ITimetableFactory SetSource(object stream)
        {
            if (!AcceptedSources.Contains(stream.GetType()))
                throw new ArgumentException("Invalid Type of source");

            str = stream as Stream;
            return this;
        }
    }
}
