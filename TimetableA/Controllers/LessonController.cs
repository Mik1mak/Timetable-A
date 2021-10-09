using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerMyBase
    {
        private readonly IGroupsRepository groupsRepo;
        private readonly ILessonsRepository lessonsRepo;
        private readonly IMapper mapper;

        public LessonController(ILessonsRepository lessonsRepo, IGroupsRepository groupsRepo, IMapper mapper, ILogger<LessonController> logger)
        {
            this.lessonsRepo = lessonsRepo;
            this.groupsRepo = groupsRepo;
            this.mapper = mapper;

            lessonsRepo.Logger = groupsRepo.Logger = logger;
        }

        [HttpPost("{groupId}")]
        [Authorize(AuthLevel.Edit, typeof(LessonAuthMethod))]
        public async Task<ActionResult<LessonOutputModel>> AddLesson(int groupId, [FromBody] LessonInputModel input)
        {
            Group group = await groupsRepo.GetAsync(groupId);

            if (group.TimetableId != ThisTimetable.Id)
                return BadRequest();

            Lesson lessonToAdd = mapper.Map<Lesson>(input);
            lessonToAdd.GroupId = groupId;

            if (group.Lessons.Any(x => x.CollideWith(lessonToAdd)))
                return BadRequest("Lesson can't collide with other existing lessons in same group.");

            if (await lessonsRepo.SaveAsync(lessonToAdd))
                return Ok(mapper.Map<LessonOutputModel>(lessonToAdd));

            return Problem();
        }


        [HttpGet("{id}")]
        [Authorize(AuthLevel.Read, typeof(LessonAuthMethod))]
        public async Task<ActionResult<GroupOutputModel>> GetLesson(int id)
        {
            Lesson lesson = await lessonsRepo.GetAsync(id);
            LessonOutputModel output = mapper.Map<LessonOutputModel>(lesson);
            return Ok(output);
        }

        [HttpGet("Group/{groupId}")]
        [Authorize(AuthLevel.Read, typeof(GroupAuthMethod), "goupId")]
        public async Task<ActionResult<IEnumerable<LessonOutputModel>>> GetLessons(int groupId)
        {
            Group group = await groupsRepo.GetAsync(groupId);

            return Ok(group.Lessons.Select(x => mapper.Map<LessonOutputModel>(x)));
        }

        [HttpPut("{id}")]
        [Authorize(AuthLevel.Edit, typeof(LessonAuthMethod))]
        public async Task<ActionResult> PutLesson(int id, [FromBody] LessonInputModel input)
        {
            Lesson lesson = await lessonsRepo.GetAsync(id);
            lesson.Name = input.Name;
            lesson.Start = input.Start;
            lesson.Duration = TimeSpan.FromMinutes(input.Duration);
            lesson.Link = input.Link;
            lesson.Classroom = input.Classroom;

            if (lesson.Group.Lessons.Any(x => x.CollideWith(lesson)))
                return BadRequest("Lesson can't collide with other existing lessons in this same group.");

            if (await lessonsRepo.SaveAsync(lesson))
                return Ok();
            return Problem();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthLevel.Edit, typeof(LessonAuthMethod))]
        public async Task<ActionResult> DeleteLesson(int id)
        {
            if (await lessonsRepo.DeleteAsync(id))
                return Ok();
            return Problem();
        }

        [HttpPost("Verify/{groupId}")]
        [Authorize(AuthLevel.Edit, typeof(LessonAuthMethod))]
        public IActionResult VerifyNewLesson(int groupId, [FromBody] LessonVerifyRequest requestBody)
        {
            Lesson lesson = mapper.Map<Lesson>(requestBody.Lesson);
            IEnumerable<Group> groups = ThisTimetable.Groups.Where(g => requestBody.GroupIds.Contains(g.Id) || g.Id == groupId);

            if (groups.Any(g => g.Lessons.Any(l => l.CollideWith(lesson))))
                return Ok(false);
            return Ok(true);
        }
    }
}
