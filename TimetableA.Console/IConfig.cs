using System;
using TimetableA.API.DTO.InputModels;

namespace TimetableA.ConsoleImporter
{
    public interface IConfig
    {
        public Uri Source { get; }
        public Uri Dest { get; }
        public Uri StaticApp { get; }
        public int Cycles { get; }

        public bool AsLayer { get; }
        public AuthenticateRequest LoginInfo { get; }
    }
}
