using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;
using TimetableA.Models;

namespace TimetableA.BlazorImporter
{
    public class TimetableSender
    {
        private readonly ITimetableEndpoints apiService;
        private AuthenticateResponse? response;
        private bool timetableCreated = false;

        public ICollection<int> AddedGroupsId { get; } = new HashSet<int>();

        public TimetableSender(ITimetableEndpoints client)
        {
            this.apiService = client;
        }

        public async Task<TimetableSender> CreateTimetable(Timetable timetable)
        {
            var slicer = new TimetableSlicer(timetable);

            response = await apiService.CreateTimetable(slicer.GetTimetableModel());
            timetableCreated = true;

            return this;
        }

        public async Task<TimetableSender> LoginToAccount(AuthenticateRequest request)
        {
            response = await apiService.Login(request);
            return this;
        }

        public async Task<AuthenticateResponse> CreateAsync(Timetable timetable)
        {
            if (response is null)
                throw new InvalidOperationException();

            var slicer = new TimetableSlicer(timetable);

            foreach (var groupAndLessons in slicer.GetTimetableBody())
            {
                var addedGroup = await apiService.AddGroup(groupAndLessons.Key, response.Token);

                AddedGroupsId.Add(addedGroup.Id);

                foreach (LessonInputModel lesson in groupAndLessons.Value)
                    await apiService.AddLesson(addedGroup.Id, lesson, response.Token);
            }

            return response;
        }

        public async Task DeleteTimetableIfWasCreated()
        {
            if (timetableCreated)
                await apiService.Delete(response.Token);
        }

    }
}
