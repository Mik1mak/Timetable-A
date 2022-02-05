using TimetableA.Importer;

namespace TimetableA.BlazorImporter
{
    public class IcsParserInfo : ITimetableParserInfo
    {
        public bool ParseFromUrl { get; init; } = true;

        public bool ParseFromFile { get; init; } = true;

        public string DisplayName { get; init; } = "ICS";

        public string NameOf => nameof(IcsParserInfo);

        public bool AskAboutCycles { get; init; } = true;

        public Task<ITimetableParser> GetParserFromStreamAsync(Stream stream)
        {
            return Task.FromResult<ITimetableParser>(new IcsParser(new StreamReader(stream)));
        }
    }

    public class OptivumParserInfo : ITimetableParserInfo
    {
        public bool ParseFromUrl { get; init; } = false;

        public bool ParseFromFile { get; init; } = true;

        public string DisplayName { get; init; } = "Optivum";

        public string NameOf => nameof(OptivumParserInfo);

        public bool AskAboutCycles { get; init; } = false;

        public async Task<ITimetableParser> GetParserFromStreamAsync(Stream stream)
        {
            string htmlDoc;
            using (StreamReader docReader = new(stream))
            {
                htmlDoc = await docReader.ReadToEndAsync();
            }
                
            return new OptivumParser(htmlDoc);
        }
    }
}
