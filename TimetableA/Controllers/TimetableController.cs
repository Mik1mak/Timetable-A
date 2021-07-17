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
        //private readonly ILogger logger;
        private readonly IMapper mapper;

        public TimetableController(ITimetableRepository timetableRepo, IGroupsRepository groupsRepo, IMapper mapper, ILogger<TimetableController> logger)
        {
            this.timetablesRepo = timetableRepo;
            this.groupsRepo = groupsRepo;
            this.mapper = mapper;
            //this.logger = logger;
            timetableRepo.Logger = groupsRepo.Logger = logger;
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

            if (group == null)
                return NotFound();
            if (group.TimetableId != id)
                return BadRequest();

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

        [HttpPut("{id}/Group/{groupId}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> PutGroup(int id, int groupId, [FromBody] GroupInputModel input)
        {
            var group = await groupsRepo.GetAsync(groupId);

            if (group == null)
                return NotFound();

            if(group.TimetableId == id)
            {
                group.Name = input.Name;
                group.HexColor = input.HexColor;

                if (await groupsRepo.SaveAsync(group))
                    return Ok();
                return Problem();
            }

            return BadRequest();
        }

        [HttpDelete("{id}/Group/{groupId}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> DeleteGroup(int id, int groupId)
        {
            var groupToDel = await groupsRepo.GetAsync(groupId);

            if (groupToDel == null)
                return NotFound();

            if(groupToDel.TimetableId == id)
            {
                if (await groupsRepo.DeleteAsync(groupId))
                    return Ok();
                return Problem();
            }

            return BadRequest();
        }
        #endregion
    }
}
