using TimetableA.Importer;
using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    public class TimetableGetter
    {
        private readonly HttpClient client;
        private readonly ITimetableEndpoints api;

        public TimetableGetter(HttpClient client, ITimetableEndpoints api)
        {
            this.client = client;
            this.api = api;
        }

        public async Task<Timetable> GetFromUri(ITimetableSourceType timetableSrc, string uri)
        {
            if(timetableSrc.AcceptedSources.Contains(typeof(Stream)))
            {
                Uri source = GetUriFromSource(uri);
                Stream stream = await GetStream(source);
                return await timetableSrc.SetSource(stream).GetTimetable();
            }
            else if(timetableSrc.AcceptedSources.Contains(typeof(TimetableApiGetter)))
            {
                TimetableApiGetter apiGetter = new(api, uri.GetLoginInfo());
                return await timetableSrc.SetSource(apiGetter).GetTimetable();
            }

            throw new Exception("Source not supproted");
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
