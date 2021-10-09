using Refit;
using System.Threading.Tasks;
using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;

namespace TimetableA.ConsoleImporter
{
    partial class Program
    {
        public interface ITimetableEndpoints
        {
            [Post("/api/Timetable")]
            Task<AuthenticateResponse> CreateTimetable([Body] TimetableInputModel timetable);

            [Post("/api/Group")]
            Task<GroupOutputModel> AddGroup([Body] GroupInputModel group, [Header("Authorization")] string token);

            [Post("/api/Lesson/{groupId}")]
            Task<LessonOutputModel> AddLesson(int groupId, [Body] LessonInputModel lesson, [Header("Authorization")] string token);
        }
    }
}
