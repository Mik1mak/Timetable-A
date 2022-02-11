using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    public class ApiSourceType : ITimetableSourceType
    {
        private TimetableApiGetter? apiGetter;

        public IEnumerable<Type> AcceptedSources { get; } = new HashSet<Type>()
        { 
            typeof(TimetableApiGetter)
        };

        public string NameOf => nameof(ApiSourceType);

        public bool ParseFromUrl => true;

        public bool ParseFromFile => false;

        public string DisplayName { get; init; } = "Copy";

        public bool AskAboutCycles { get; init; } = false;

        public async Task<Timetable> GetTimetable()
        {
            if (apiGetter == null)
                throw new InvalidOperationException();

            return await apiGetter.GetTimetableFromApi();
        }

        public ITimetableFactory SetSource(object source)
        {
            apiGetter = source as TimetableApiGetter;

            if (apiGetter == null)
                throw new ArgumentException("Invalid Type of Source");

            return this;
        }
    }
}
