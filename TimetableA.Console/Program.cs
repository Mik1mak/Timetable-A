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

            StreamReader source = GetSoure(config.Source).Result;

            Console.WriteLine("Tworzenie planu...");
            Timetable timetable = new IcsParser(source).GetTimetable();

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

        private static async Task<StreamReader> GetSoure(Uri uri)
        {
            StreamReader output;

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
                    Console.WriteLine("Pobieranie planu...");
                    output = new StreamReader(await new HttpClient().GetStreamAsync(uri));
                    break;
                case "file":
                    Console.WriteLine("Otwieranie planu...");
                    output = File.OpenText(uri.LocalPath);
                    break;
                default:
                    Console.WriteLine("Invalid Uri.");
                    throw new Exception("Invalid Uri.");
            }

            return output;
        }
    }
}
