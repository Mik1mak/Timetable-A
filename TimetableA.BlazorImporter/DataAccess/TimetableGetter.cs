using TimetableA.Importer;
using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    public class TimetableGetter
    {
        private readonly HttpClient client;

        public TimetableGetter(HttpClient client) => this.client = client;

        public async Task<Timetable> GetFromUri(ITimetableParserInfo parserInfo, string uri)
        {
            Uri source = GetUriFromSource(uri);
            Stream stream = await GetStream(source);
            ITimetableParser parser = await parserInfo.GetParserFromStreamAsync(stream);
            return parser.GetTimetable();
        }

        private Uri GetUriFromSource(string source)
        {
            Uri uri = new(source);

            if (uri.Scheme == "webcals" || uri.Scheme == "webcal")
            {
                var uriBuilder = new UriBuilder(uri)
                {
                    Scheme = Uri.UriSchemeHttp,
                    Port = -1,
                };

                uri = uriBuilder.Uri;
            }

            return uri;
        }

        private async Task<Stream> GetStream(Uri uri)
        {
            switch (uri.Scheme)
            {
                case "http":
                case "https":
                    try
                    {
                        var stream = await client.GetStreamAsync(uri);
                        return stream;
                    }
                    catch
                    {
                        var uriBuilder = new UriBuilder(uri)
                        {
                            Scheme = uri.Scheme == Uri.UriSchemeHttps ?
                                Uri.UriSchemeHttp : Uri.UriSchemeHttps,
                            Port = -1,
                        };

                        uri = uriBuilder.Uri;

                        return await client.GetStreamAsync(uri);
                    }
                default:
                    throw new Exception("Invalid source Uri.");
            }
        }
    }
}
