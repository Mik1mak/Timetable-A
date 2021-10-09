using System;
using System.IO;

namespace TimetableA.ConsoleImporter
{
    public class TxtConfig : IConfig
    {
        public TxtConfig()
        {
            using var reader = File.OpenText("Config.txt");

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] sLine = line.Split("=>");

                if(sLine[0].Contains("Weeks"))
                    Cycles = int.Parse(sLine[^1]);
                else if(sLine[0].Contains(nameof(Source)))
                    Source = new(sLine[^1]);
                else if (sLine[0].Contains(nameof(Dest)))
                    Dest = new(sLine[^1]);
                else if (sLine[0].Contains(nameof(StaticApp)))
                    StaticApp = new(sLine[^1]);
            }
        }

        public Uri Source { get; private set; }

        public Uri Dest { get; private set; } = new(@"https://timetablea-api.azurewebsites.net");

        public Uri StaticApp { get; private set; } = new(@"https://timetableappstatic.z22.web.core.windows.net");

        public int Cycles { get; private set; }
    }
}
