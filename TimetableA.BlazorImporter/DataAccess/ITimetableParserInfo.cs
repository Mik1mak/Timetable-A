using TimetableA.Importer;

namespace TimetableA.BlazorImporter
{
    public interface ITimetableParserInfo
    {
        public string NameOf { get; }
        public bool ParseFromUrl { get; }
        public bool ParseFromFile { get; }
        public string DisplayName { get; }
        public bool AskAboutCycles { get; }
    }
}
