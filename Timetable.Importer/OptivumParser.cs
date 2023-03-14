using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Models;

namespace TimetableA.Importer
{
    public class OptivumParser : ITimetableParser
    {
        private const string DEFAULT_GROUP_NAME = "Zajęcia";
        private readonly HtmlDocument doc = new();
        private readonly Dictionary<string, Group> groups = new();
        private Timetable timetable = new()
        {
            Cycles = 1,
            DisplayEmptyDays = false,
        };

        public OptivumParser(Stream source)
        {
            doc.Load(source);
        }

        public OptivumParser(string htmlDoc)
        {
            doc.LoadHtml(htmlDoc);
        }

        public Task<Timetable> GetTimetable()
        {
            Parse();
            return Task.FromResult(timetable);
        }

        private void Parse()
        {
            string tab = "table";
            //var htmlDoc = doc.DocumentNode.InnerHtml;

            var tabNode = doc.DocumentNode.SelectSingleNode("/html/body/table");
            if (tabNode.LastChild.OriginalName == "tbody")
                tab = "table/tbody";

            HtmlNode node = doc.DocumentNode.SelectSingleNode($"/html/body/{tab}/tr/td/span");

            timetable.Name = node.InnerHtml;
            HtmlNodeCollection cellList = doc.DocumentNode.SelectNodes($"//div/{tab}/tr/td/{tab}/tr/td");

            if (cellList is null)
                return;

            for (int i = 0; i < cellList.Count; i++)
            {
                if (cellList[i].HasClass("nr"))
                {
                    int addedLessons = 0;
                    string hourStr = cellList[i + 1].InnerHtml;

                    for (int k = 1; k <= 5; k++)
                    {
                        if (cellList[i + k + 1].HasClass("l"))
                        {
                            CellInput(ParseHourStr(k, hourStr), cellList[i + k + 1]);
                            addedLessons++;
                        }

                    }
                    i += addedLessons;
                }
            }

            timetable.Groups = groups.Select(g => g.Value).OrderBy(g => g.Name).ToList();
        }

        private static (DateTime, TimeSpan) ParseHourStr(int dayOfWeek, string str)
        {
            string[] splitedHourStr = str.Split('-', ':');

            DateTime dayDate = DateTime.MinValue.AddDays((dayOfWeek + 6) % 7);

            DateTime start = dayDate
                .AddHours(int.Parse(splitedHourStr[0]))
                .AddMinutes(int.Parse(splitedHourStr[1]));
            DateTime stop = dayDate
                .AddHours(int.Parse(splitedHourStr[^2]))
                .AddMinutes(int.Parse(splitedHourStr[^1]));

            return (start, stop - start);
        }

        private void CellInput((DateTime, TimeSpan) time, HtmlNode cell) //TODO: wyjątki
        {
            if (cell.Element("span") is null)
                return;
            
            string name, classroom, group, teacher;

            string[] splitedLessons = cell.InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
            foreach (string item in splitedLessons)
            {
                HtmlDocument htmlScrap = new();
                htmlScrap.LoadHtml(item);

                HtmlNode node = htmlScrap.DocumentNode;

                HtmlNodeCollection temp;

                if (node.FirstChild.Attributes["style"] is null && node.ChildNodes.Count == 5)
                {
                    temp = node.ChildNodes;
                    name = temp[0].InnerHtml.Trim(' ');
                    group = temp[1].InnerText.Trim('-').Trim(' ');
                }
                else
                {
                    temp = node.FirstChild.ChildNodes;
                    name = temp[0].InnerHtml.Split('-')[0].Trim(' ');
                    group = temp[0].InnerText.Split('-')[1].Trim(' ');
                }

                //teacher = temp[2].InnerHtml.Trim(' ');
                classroom = temp[4].InnerHtml.Trim(' ');

                if (string.IsNullOrEmpty(group))
                    group = DEFAULT_GROUP_NAME;

                Lesson lesson = new()
                {
                    Name = name,
                    Classroom = classroom,
                    Start = time.Item1,
                    Duration = time.Item2,
                };
                AddLesson(group, lesson);
            }
        }

        private void AddLesson(string groupName, Lesson lesson)
        {
            if (groups.ContainsKey(groupName))
                groups[groupName].Lessons.Add(lesson);
            else
                groups.Add(groupName, new Group
                {
                    Name = groupName,
                    Lessons = new List<Lesson>(),
                    HexColor = "#FFFFFF",
                }); 
        }
    }
}
