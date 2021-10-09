using Refit;
using System.Net.Http;
using System.Threading.Tasks;
using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;
using TimetableA.Models;

namespace TimetableA.ConsoleImporter
{
    partial class Program
    {
        public class TimetableSender
        {
            private readonly ITimetableEndpoints apiService;

            public TimetableSender(HttpClient client)
            {
                this.apiService = RestService.For<ITimetableEndpoints>(client);
            }

            public async Task<AuthenticateResponse> CreateAsync(Timetable timetable)
            {
                var slicer = new TimetableSlicer(timetable);
                AuthenticateResponse response = await apiService.CreateTimetable(slicer.GetTimetableModel());
                string token = response.Token;

                foreach (var groupAndLessons in slicer.GetTimetableBody())
                {
                    var group = await apiService.AddGroup(groupAndLessons.Key, token);

                    foreach (LessonInputModel lesson in groupAndLessons.Value)
                        await apiService.AddLesson(group.Id, lesson, token);
                }

                return response;
            }
        }
    }
}
