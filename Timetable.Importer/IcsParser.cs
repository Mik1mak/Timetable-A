using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TimetableA.Models;

namespace TimetableA.Importer
{
    public class IcsParser : ITimetableParser
    {
        private readonly Timetable timetable = new();

        private readonly ICollection<ILineParser> lineParsers;
        private readonly StreamReader source;

        private bool parsed = false;

        public IcsParser(StreamReader source)
        {
            var defaultGroup = new Group
            {
                Name = "Przedmioty",
                HexColor = "#FFFFFF",
                Lessons = new List<Lesson>()
            };

            timetable.Groups = new List<Group>()
            {
                defaultGroup
            };

            lineParsers = new HashSet<ILineParser>()
            {
                new TimetableLineParser(timetable),
                new LessonLineParser(defaultGroup),
            };

            this.source = source;
        }

        public async Task<Timetable> GetTimetable()
        {
            if(!parsed)
            {
                using (source)
                {
                    string line;
                    while ((line = await source.ReadLineAsync()) != null)
                    {
                        foreach (ILineParser p in lineParsers)
                            p.Parse(line);
                    }
                }
                parsed = true;
            }

            return timetable;
        }
    }
}
