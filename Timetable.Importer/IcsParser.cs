using System.Collections.Generic;
using System.IO;
using TimetableA.Models;

namespace TimetableA.Importer
{
    public class IcsParser : ITimetableParser
    {
        private readonly Timetable timetable = new();

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

            var lineParsers = new HashSet<ILineParser>()
            {
                new TimetableLineParser(timetable),
                new LessonLineParser(defaultGroup),
            };

            using(source)
            {
                string line;
                while ((line = source.ReadLine()) != null)
                {
                    foreach(ILineParser p in lineParsers)
                        p.Parse(line);
                }
            }
        }

        public Timetable GetTimetable()
        {
            return timetable;
        }
    }
}
