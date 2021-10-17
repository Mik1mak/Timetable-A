using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TimetableA.API.DTO.OutputModels;
using TimetableA.Models;
using TimetableA.Importer;
using System.Linq;

namespace TimetableA.ConsoleImporter
{
    partial class Program
    {
        private static readonly HttpClient client = new();
        private const string CONFIGNAME = "config.txt";

        static async Task Main(string[] args)
        {
            if(!File.Exists(CONFIGNAME))
            {
                Console.WriteLine("Brak pliku konfiguracyjnego.");
                End();
                return;
            }

            IConfig config = null;
            TimetableSender sender = new(client);

            try
            {
                config = new TxtConfig(CONFIGNAME);
                StreamReader source = await GetReaderFromSource(config.Source);
                client.BaseAddress = config.Dest;

                Console.WriteLine("Tworzenie planu...");
                Timetable timetable = new IcsParser(source).GetTimetable();

                timetable.Cycles = config.Cycles;
                timetable.Name = timetable.Name.SliceIfTooLong(32);
                timetable.TrimLessonsToCycles();

                Console.WriteLine("Wysyłanie planu...");

                if(config.AsLayer)
                {
                    await sender.LoginToAccount(config.LoginInfo);
                    timetable.Groups.First().Name = timetable.Name;
                }
                else
                {
                    await sender.CreateTimetable(timetable);
                }

                AuthenticateResponse response = await sender.CreateAsync(timetable);

                string output = $"\r\nLink do planu: {config.StaticApp}login/?id={response.Id}" +
                    $"&key={response.EditKey}" +
                    $"&returnUrl=%2F%3F{string.Join("%26", sender.AddedGroupsId.Select(id => $"g%3D{id}"))}";

                Console.WriteLine($"Plan zapisany. {output}");
                File.AppendAllText("Output.txt", output);
            }
            catch(Refit.ApiException ex)
            {
                if(config != null && !config.AsLayer)
                    await sender.DeleteTimetableIfWasCreated();

                Console.WriteLine($"Request Error: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            End();
        }

        private static void End()
        {
            Console.WriteLine("Enter any key to exit.");
            Console.ReadKey();
        }

        private static async Task<StreamReader> GetReaderFromSource(Uri uri)
        {
            if(uri.Scheme == "webcals")
            {
                var uriBuilder = new UriBuilder(uri)
                {
                    Scheme = Uri.UriSchemeHttp,
                    Port = -1
                };

                uri = uriBuilder.Uri;
            }

            switch (uri.Scheme)
            {
                case "http":
                case "https":
                    HttpClient client = new();
                    Console.WriteLine("Pobieranie planu...");

                    try
                    {
                        return new StreamReader(await client.GetStreamAsync(uri));
                    }
                    catch (Exception)
                    {
                        var uriBuilder = new UriBuilder(uri)
                        {
                            Scheme = uri.Scheme == Uri.UriSchemeHttps ? 
                                Uri.UriSchemeHttp : Uri.UriSchemeHttps,
                            Port = -1,
                        };

                        uri = uriBuilder.Uri;

                        return new StreamReader(await client.GetStreamAsync(uri));
                    }
                case "file":
                    Console.WriteLine("Otwieranie planu...");
                    return File.OpenText(uri.LocalPath);
                default:
                    Console.WriteLine("Invalid Uri.");
                    throw new Exception("Invalid source Uri.");
            }
        }
    }
}
