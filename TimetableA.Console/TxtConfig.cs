using System;
using System.IO;
using TimetableA.API.DTO.InputModels;

namespace TimetableA.ConsoleImporter
{
    public class TxtConfig : IConfig
    {
        public TxtConfig(string path)
        {
            using var reader = File.OpenText(path);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] sLine = line.Split("=>");

                try
                { 
                    if (sLine[0].Contains(nameof(Cycles)))
                        Cycles = int.Parse(sLine[^1]);
                    else if (sLine[0].Contains(nameof(Source)))
                        Source = new(sLine[^1]);
                    else if (sLine[0].Contains(nameof(Dest)))
                        Dest = new(sLine[^1]);
                    else if (sLine[0].Contains(nameof(StaticApp)))
                        StaticApp = new(sLine[^1]);
                    else if (sLine[0].Contains(nameof(AsLayer)))
                        AsLayer = bool.Parse(sLine[^1]);
                    else if (sLine[0].Contains(nameof(LoginInfo.Id)))
                        LoginInfo.Id = sLine[^1];
                    else if (sLine[0].Contains(nameof(LoginInfo.Key)))
                        LoginInfo.Key = sLine[^1];
                }catch(Exception ex)
                {
                    throw new FileLoadException($"Faild to load {path}. Can't read ${sLine[0]} value", ex);
                }
            }
        }

        public Uri Source { get; private set; }

        public Uri Dest { get; private set; } = new(@"https://timetablea-api.azurewebsites.net");

        public Uri StaticApp { get; private set; } = new(@"https://timetableappstatic.z22.web.core.windows.net");

        public int Cycles { get; private set; }

        public bool AsLayer { get; private set; } = false;

        public AuthenticateRequest LoginInfo { get; private set; } = new();
    }
}
