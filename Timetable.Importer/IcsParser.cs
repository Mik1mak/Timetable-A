using System.Collections.Generic;
using System.IO;
using TimetableA.Models;

namespace TimetableA.Importer
{
    public class IcsParser : ITimetableParser
    {
        private readonly Timetable timetable = new();

        protected IcsParser(StreamReader reader)
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

            using(reader)
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach(ILineParser p in lineParsers)
                        p.Parse(line);
                }
            }
        }

        public static IcsParser FromFile(string path)
        {
            return new IcsParser(File.OpenText(path));
        }

        public Timetable GetTimetable()
        {
            return timetable;
        }
    }
}
