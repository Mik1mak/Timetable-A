namespace TimetableA.BlazorImporter
{
    public interface ITimetableSourceType : ITimetableFactory, ITimetableParserInfo
    {
        public IEnumerable<Type> AcceptedSources { get; }
    }
}
