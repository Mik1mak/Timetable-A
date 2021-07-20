using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.API.Models.InputModels;
using TimetableA.API.Models.OutputModels;
using TimetableA.API.Services;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimetableA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableRepository timetablesRepo;
        private readonly IGroupsRepository groupsRepo;
        private readonly ILessonsRepository lessonsRepo;
        //private readonly ILogger logger;
        private readonly IMapper mapper;

        public TimetableController(ITimetableRepository timetablesRepo, IGroupsRepository groupsRepo, ILessonsRepository lessonsRepo,
            IMapper mapper, ILogger<TimetableController> logger)
        {
            this.timetablesRepo = timetablesRepo;
            this.groupsRepo = groupsRepo;
            this.lessonsRepo = lessonsRepo;
            this.mapper = mapper;
            //this.logger = logger;
            timetablesRepo.Logger = groupsRepo.Logger = lessonsRepo.Logger = logger;
        }

        #region Timetable
        [HttpGet]
#if DEBUG == false
        [Authorize(AuthLevel.Admin)]
#endif
        public async Task<ActionResult<IEnumerable<Timetable>>> Get()
        {
            var timetable = await timetablesRepo.GetAllAsync();

            if (timetable == null)
                return NotFound();
            if (!timetable.Any())
                return NotFound();


            //var output = timetable.Select(x =>
            //{
            //    //x.Gropus?.Clear();
            //    return mapper.Map<TimetableOutputModel>(x);
            //});

            return Ok(timetable);
        }

        [HttpGet("{id}")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<TimetableOutputModel>> Get(int id)
        {
            var timetable = await timetablesRepo.GetAsync(id);

            if (timetable == null)
                return NotFound();

            var output = mapper.Map<TimetableOutputModel>(timetable);

            return Ok(output);
        }

        [HttpPost]
        public async Task<ActionResult<Timetable>> Post(TimetableInputModel input)
        {
            if (input == null)
                return BadRequest("Timetable can't be null.");

            if (!ModelState.IsValid)
                return BadRequest();

            var newTimetable = mapper.Map<Timetable>(input);

            newTimetable.CreateDate = DateTime.Now;
            newTimetable.EditKey = KeyGen.Generate();
            newTimetable.ReadKey = KeyGen.Generate();

            if (await timetablesRepo.SaveAsync(newTimetable))
                return Ok(newTimetable);
                

            return Problem();
        }

        [HttpPut("{id}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> Put(int id, [FromBody] TimetableInputModel input)
        {
            if (input == null)
                return BadRequest("Timetable can't be null");

            var timetable = await timetablesRepo.GetAsync(id);

            if (timetable == null)
                return NotFound();

            timetable.Name = input.Name;
            timetable.Cycles = input.Cycles;

            if (await timetablesRepo.SaveAsync(timetable))
                return Ok();

            return Problem();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> Delete(int id)
        {
            if (await timetablesRepo.DeleteAsync(id))
                return Ok();

            return Problem();
        }
        #endregion

        #region Groups
        [HttpPost("{id}/Group")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult<GroupOutputModel>> AddGroup(int id, [FromBody] GroupInputModel input)
        {
            if (input == null)
                return BadRequest("Group can't be null.");

            if (!ModelState.IsValid)
                return BadRequest();

            var newGroup = mapper.Map<Group>(input);
            newGroup.TimetableId = id;

            if (await groupsRepo.SaveAsync(newGroup))
                return Ok(mapper.Map<GroupOutputModel>(newGroup));

            return Problem();
        }

        [HttpGet("{id}/Group/{groupId}")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<GroupOutputModel>> GetGroup(int id, int groupId)
        {
            var group = await groupsRepo.GetAsync(groupId);

            var result = ValidId(id, group);
            if (result != null)
                return result;

            var output = mapper.Map<GroupOutputModel>(group);
            return Ok(output);
        }

        [HttpGet("{id}/Groups")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<IEnumerable<GroupOutputModel>>> GetGroups(int id)
        {
            var timetable = await timetablesRepo.GetAsync(id);

            if (timetable == null)
                return NotFound();
            if (timetable.Gropus == null)
                return NotFound();
            if(!timetable.Gropus.Any())
                return NotFound();

            return Ok(timetable.Gropus.Select(x => mapper.Map<GroupOutputModel>(x)));
        }

        [HttpGet("{id}/CollidingGroups/{groupId}")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<IEnumerable<int>>> CollidingGroups(int id, int groupId)
        {
            var group = await groupsRepo.GetAsync(groupId);

            var result = ValidId(id, group);
            if (result != null)
                return result;

            var otherGroups = (await timetablesRepo.GetAsync(id)).Gropus?.Where(x => x.Id != group.Id);

            return Ok(group.CollidingGroupsIds(otherGroups));
        }

        [HttpPut("{id}/Group/{groupId}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> PutGroup(int id, int groupId, [FromBody] GroupInputModel input)
        {
            var group = await groupsRepo.GetAsync(groupId);

            var result = ValidId(id, group);
            if (result != null)
                return result;

            group.Name = input.Name;
            group.HexColor = input.HexColor;

            if (await groupsRepo.SaveAsync(group))
                return Ok();
            return Problem();
        }

        [HttpDelete("{id}/Group/{groupId}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> DeleteGroup(int id, int groupId)
        {
            var groupToDel = await groupsRepo.GetAsync(groupId);

            var result = ValidId(id, groupToDel);
            if (result != null)
                return result;

            if (await groupsRepo.DeleteAsync(groupId))
                return Ok();
            return Problem();
        }
        #endregion

        #region Lessons
        [HttpPost("{id}/Group/{groupId}/Lesson")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult<LessonOutputModel>> AddLesson(int id, int groupId, [FromBody] LessonInputModel input)
        {
            if (input == null)
                return BadRequest("Lesson can't be null.");

            if (!ModelState.IsValid)
                return BadRequest();

            var group = await groupsRepo.GetAsync(groupId);

            var result = ValidId(id, group);
            if (result != null)
                return result;

            var lessonToAdd = mapper.Map<Lesson>(input);
            lessonToAdd.GroupId = groupId;

            if (group.Lessons.Any(x => x.CollidesWith(lessonToAdd)))
                return BadRequest("Lesson can't collide with other existing lessons in same group.");

            if (await lessonsRepo.SaveAsync(lessonToAdd))
                return Ok(lessonToAdd);

            return Problem();
        }


        [HttpGet("{id}/Group/{groupId}/Lesson/{lessonId}")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<GroupOutputModel>> GetLesson(int id, int groupId, int lessonId)
        {
            var group = await groupsRepo.GetAsync(groupId);

            var result = ValidId(id, group, lessonId);
            if (result != null)
                return result;

            var output = mapper.Map<LessonOutputModel>(group.Lessons.First(x => x.Id == lessonId));
            return Ok(output);
        }

        [HttpGet("{id}/Groups/{groupId}/Lessons")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<IEnumerable<LessonOutputModel>>> GetLessons(int id, int groupId)
        {
            var group = await groupsRepo.GetAsync(groupId);

            var result = ValidId(id, group);
            if (result != null)
                return result;

            return Ok(group.Lessons.Select(x => mapper.Map<LessonOutputModel>(x)));
        }

        [HttpPut("{id}/Group/{groupId}/Lesson/{lessonId}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> PutLesson(int id, int groupId, int lessonId, [FromBody] LessonInputModel input)
        {
            var group = await groupsRepo.GetAsync(groupId);

            var result = ValidId(id, group, lessonId);
            if (result != null)
                return result;

            var lesson = await lessonsRepo.GetAsync(lessonId);
            lesson.Name = input.Name;
            lesson.Start = input.Start;
            lesson.Duration = TimeSpan.FromMinutes(input.Duration);
            lesson.Link = input.Link;
            lesson.Classroom = input.Classroom;

            if (group.Lessons.Any(x => x.CollidesWith(lesson)))
                return BadRequest("Lesson can't collide with other existing lessons in this same group.");

            if (await lessonsRepo.SaveAsync(lesson))
                return Ok();
            return Problem();
        }

        [HttpDelete("{id}/Group/{groupId}/Lesson/{lessonId}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> DeleteLesson(int id, int groupId, int lessonId)
        {
            var group = await groupsRepo.GetAsync(groupId);
            var result = ValidId(id, group, lessonId);
            if (result != null)
                return result;

            if (await lessonsRepo.DeleteAsync(lessonId))
                return Ok();
            return Problem();
        }

        private ActionResult ValidId(int timetableId, Group group, int? lessonId = null)
        {
            if (group == null)
                return NotFound();

            if (group.TimetableId != timetableId)
                return BadRequest();

            if(lessonId != null)
            {
                if (group.Lessons != null)
                {
                    if (!group.Lessons.Any(x => x.Id == lessonId))
                        return NotFound();
                }
                else
                    return NotFound();
            }

            return null;
        }
        #endregion
    }
}
