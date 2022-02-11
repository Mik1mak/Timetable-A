using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;
using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    internal class TimetableApiGetter
    {
        private ITimetableEndpoints api;
        private AuthenticateRequest loginInfo;

        public TimetableApiGetter(ITimetableEndpoints api, AuthenticateRequest loginInfo)
        {
            this.api = api;
            this.loginInfo = loginInfo;
        }

        public async Task<Timetable> GetTimetableFromApi()
        {
            AuthenticateResponse response = await api.Login(loginInfo);
            TimetableOutputModel timetable = await api.GetTimetable(response.Token);

            Timetable output = new()
            {
                Cycles = response.Cycles,
                DisplayEmptyDays = response.DisplayEmptyDays,
                Name = $"{response.Name} Copy",
                Groups = new HashSet<Group>(),
            };

            foreach (GroupOutputModel group in timetable.Groups)
            {
                output.Groups.Add(new Group()
                {
                    HexColor = group.HexColor,
                    Name = group.Name,
                    Lessons = group.Lessons.Select(l => new Lesson()
                    {
                        Classroom = l.Classroom,
                        Name = l.Name,
                        Link = l.Link,
                        Start = l.Start,
                        Duration = TimeSpan.FromMinutes(l.Duration),
                    }).ToList(),
                });
            }

            return output;
        }
    }
}
