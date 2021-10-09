using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TimetableA.API.DTO.OutputModels;
using TimetableA.Models;
using TimetableA.Importer;

namespace TimetableA.ConsoleImporter
{

    partial class Program
    {
        private static readonly HttpClient client = new();

        static void Main(string[] args)
        {
            IConfig config = new TxtConfig();
            client.BaseAddress = config.Dest;

            string filePath = GetFile(config.Source).Result;

            Console.WriteLine("Tworzenie planu...");
            Timetable timetable = IcsParser
                .FromFile(filePath)
                .GetTimetable();

            timetable.Cycles = config.Cycles;
            timetable.Name = timetable.Name.Substring(0, 31);
            timetable.TrimLessonsToCycles();
            
            var sender = new TimetableSender(client);

            Console.WriteLine("Wysyłanie planu...");
            AuthenticateResponse response = sender.CreateAsync(timetable).Result;
            string output = $"Link do planu: {config.StaticApp}login/?id={response.Id}&key={response.EditKey}";

            Console.WriteLine($"Plan zapisany. {output}");
            File.AppendAllText("Output.txt", output);
            Console.ReadKey();
        }

        private static async Task<string> GetFile(Uri uri)
        {
            string filePath;

            if(uri.Scheme.Equals("webcals"))
            {
                var uriBuilder = new UriBuilder(uri)
                {
                    Scheme = Uri.UriSchemeHttps,
                    Port = -1
                };

                uri = uriBuilder.Uri;
            }

            switch (uri.Scheme)
            {
                case "http":
                case "https":
                    var currentPath = Directory.GetCurrentDirectory();
                    filePath = $"{currentPath}/timetable.ics";

                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    Console.WriteLine("Pobieranie planu...");
                    using (FileStream fs = File.Create(filePath))
                    using (var response = await new HttpClient().GetStreamAsync(uri))
                        await response.CopyToAsync(fs);
                    break;
                case "file":
                    filePath = uri.LocalPath;
                    break;
                default:
                    Console.WriteLine("Invalid Uri.");
                    throw new Exception("Invalid Uri.");
            }

            return filePath;
        }
    }
}
