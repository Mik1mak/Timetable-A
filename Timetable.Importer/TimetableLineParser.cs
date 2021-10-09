using TimetableA.Models;

namespace TimetableA.Importer
{
    public class TimetableLineParser : ILineParser
    {
        private readonly Timetable timetable;
        private bool active = true;

        public TimetableLineParser(Timetable timetable) => this.timetable = timetable;

        public void Parse(string line)
        {
            if (!Continue(line))
                return;

            if (line.Contains("X-WR-CALNAME"))
            {
                string[] sLine = line.Split(':');
                timetable.Name = sLine[^1];
            }
        }

        private bool Continue(string line)
        {
            if (!active)
            {
                return false;
            }

            if (line.Contains("VEVENT"))
            {
                active = false;
                return false;
            }

            return active;
        }
    }
}
