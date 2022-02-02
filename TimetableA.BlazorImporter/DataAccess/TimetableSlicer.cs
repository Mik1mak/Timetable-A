using System.Collections.Generic;
using System.Linq;
using TimetableA.API.DTO.InputModels;
using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    public class TimetableSlicer
    {
        private readonly Timetable timetable;
        public TimetableSlicer(Timetable timetable)
        {
            this.timetable = timetable;
        }

        public TimetableInputModel GetTimetableModel()
        {
            return new TimetableInputModel
            {
                ShowWeekend = true,
                Cycles = timetable.Cycles,
                Name = timetable.Name,
            };
        }

        public IDictionary<GroupInputModel, IEnumerable<LessonInputModel>> GetTimetableBody()
        {
            var output = new Dictionary<GroupInputModel, IEnumerable<LessonInputModel>>();

            foreach (Group g in timetable.Groups)
            {
                output.Add(new GroupInputModel
                {
                    Name = g.Name.SliceIfTooLong(32),
                    HexColor = g.HexColor,
                }, 
                g.Lessons.Select(l => {
                    return new LessonInputModel
                    {
                        Name = l.Name.SliceIfTooLong(32),
                        Start = l.Start,
                        Duration = (int)l.Duration.TotalMinutes,
                        Classroom = l.Classroom?.SliceIfTooLong(32),
                        Link = l.Link?.SliceIfTooLong(512),
                    };
                }));
            }

            return output;
        }
    }
}
