using Refit;
using System.Threading.Tasks;
using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;

namespace TimetableA.BlazorImporter
{
    public interface ITimetableEndpoints
    {
        [Post("/Authenticate/Auth")]
        Task<AuthenticateResponse> Login([Body] AuthenticateRequest request);

        [Post("/api/Timetable")]
        Task<AuthenticateResponse> CreateTimetable([Body] TimetableInputModel timetable);

        [Delete("/api/Timetable")]
        Task Delete([Header("Authorization")] string token);

        [Post("/api/Group")]
        Task<GroupOutputModel> AddGroup([Body] GroupInputModel group, [Header("Authorization")] string token);

        [Post("/api/Lesson/{groupId}")]
        Task<LessonOutputModel> AddLesson(int groupId, [Body] LessonInputModel lesson, [Header("Authorization")] string token);
    }
}
