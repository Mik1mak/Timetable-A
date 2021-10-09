using System;

namespace TimetableA.ConsoleImporter
{
    public interface IConfig
    {
        public Uri Source { get; }
        public Uri Dest { get; }
        public Uri StaticApp { get; }
        public int Cycles { get; }
    }
}
