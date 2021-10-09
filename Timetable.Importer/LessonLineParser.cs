using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Models;

namespace TimetableA.Importer
{
    public class LessonLineParser : ILineParser
    {
        private readonly Group targetGroup;
        private Lesson lesson;
        private const string KEY = "VEVENT";
        private const string DATE_FORMAT = "yyyyMMdd'T'HHmmss";

        private DateTime referDateTime = DateTime.MinValue.Date;
        private bool isDescription = false;
        private readonly StringBuilder descriptionBuffer = new();

        public LessonLineParser(Group targetGroup) => this.targetGroup = targetGroup;

        public void Parse(string line)
        {
            if (line.Equals($"BEGIN:{KEY}"))
            {
                lesson = new Lesson();
                return;
            }
            else if (line.Equals($"END:{KEY}"))
            {
                targetGroup.Lessons.Add(lesson);
                return;
            }

            if(isDescription)
            {
                if(char.IsWhiteSpace(line[0]))
                {
                    descriptionBuffer.Append(line.Replace('\n', ' ').Trim());
                    return;
                }
                else
                {
                    InterpreteDesc();
                    isDescription = false;
                }
            }
            else if (line.Contains("DESCRIPTION"))
            {
                isDescription = true;
                descriptionBuffer.Append(
                    line.Replace("DESCRIPTION:", null)
                        .Replace('\n', ' ')
                        .Trim());
                return;
            }

            string[] sLine = line.Split(':');

            if (sLine[0].Contains("SUMMARY"))
                lesson.Name = sLine[^1];
            else if(sLine[0].Contains("DTSTART"))
                lesson.Start = MakeTimetableDateTime(sLine[^1]);
            else if(sLine[0].Contains("DTEND"))
                lesson.Duration = MakeTimetableDateTime(sLine[^1]) - lesson.Start;
        }

        private void InterpreteDesc()
        {
            string[] descLines = descriptionBuffer.ToString().Split(@"\n");

            lesson.Classroom = descLines[0];

            foreach (string line in descLines)
                if (line.Contains("http"))
                    lesson.Link = line;

            descriptionBuffer.Clear();
        }

        private DateTime MakeTimetableDateTime(string dateTime)
        {
            var inputTime = DateTime.ParseExact(dateTime, DATE_FORMAT, null);

            if (referDateTime == DateTime.MinValue)
            {
                referDateTime = inputTime
                    .Date.AddDays(((int)inputTime.DayOfWeek + 6) % 7);
            }
                
            DateTime output = DateTime.MinValue.Date;
            output += inputTime.TimeOfDay;

            output = output.AddDays((int)(inputTime - referDateTime).TotalDays);

            return output;
        }
    }
}
