﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.API.Models.InputModels;
using TimetableA.API.Models.OutputModels;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Entities.Models;

namespace TimetableA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerMyBase
    {
        private readonly ITimetableRepository timetablesRepo;
        private readonly IGroupsRepository groupsRepo;
        private readonly IMapper mapper;
        private readonly AppSettings settings;

        public GroupController(ITimetableRepository timetablesRepo, IGroupsRepository groupsRepo,
            IMapper mapper, ILogger<GroupController> logger, IOptions<AppSettings> settings)
        {
            this.timetablesRepo = timetablesRepo;
            this.groupsRepo = groupsRepo;
            this.mapper = mapper;
            this.settings = settings.Value;

            timetablesRepo.Logger = groupsRepo.Logger = logger;
        }

        [HttpPost]
        [Authorize(AuthLevel.Edit, typeof(GroupAuthMethod))]
        public async Task<ActionResult<GroupOutputModel>> AddGroup([FromBody] GroupInputModel input)
        {
            if (ThisTimetable.Groups.Count >= settings.MaxGroupsPerTimetable)
                return BadRequest($"Max count of groups is {settings.MaxGroupsPerTimetable}");

            var newGroup = mapper.Map<Group>(input);
            newGroup.TimetableId = ThisTimetable.Id;

            if (await groupsRepo.SaveAsync(newGroup))
            {
                var output = mapper.Map<GroupOutputModel>(newGroup);
                output.CollidingGroups = await CollidingGroups(newGroup);
                return Ok(output);
            }

            return Problem();
        }

        [HttpGet("{id}")]
        [Authorize(AuthLevel.Read, typeof(GroupAuthMethod))]
        public async Task<ActionResult<GroupOutputModel>> GetGroup(int id)
        {
            var group = await groupsRepo.GetAsync(id);

            var output = mapper.Map<GroupOutputModel>(group);
            output.CollidingGroups = await CollidingGroups(group);

            return Ok(output);
        }

        [HttpGet]
        [Authorize(AuthLevel.Read, typeof(GroupAuthMethod))]
        public async Task<ActionResult<IEnumerable<GroupOutputModel>>> GetGroups()
        {
            var timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);

            if (timetable.Groups == null)
                return NotFound();
            //if (!timetable.Groups.Any())
            //    return NotFound();

            var output = new List<GroupOutputModel>();

            foreach (var g in timetable.Groups)
            {
                var outputModel = mapper.Map<GroupOutputModel>(g);
                outputModel.CollidingGroups = await CollidingGroups(g);
                output.Add(outputModel);
            }

            return Ok(output);
        }

        [HttpGet("{id}/CollidingGroups")]
        [Authorize(AuthLevel.Read, typeof(GroupAuthMethod))]
        public async Task<ActionResult<IEnumerable<int>>> CollidingGroups(int id)
        {
            var group = await groupsRepo.GetAsync(id);
            return Ok(await CollidingGroups(group));
        }

        private async Task<IEnumerable<int>> CollidingGroups(Group group)
        {
            var timetable = await timetablesRepo.GetAsync(group.TimetableId);
            var otherGroups = timetable.Groups?.Where(x => x.Id != group.Id);

            return group.CollidingGroupsIds(otherGroups);
        }

        [HttpPut("{id}")]
        [Authorize(AuthLevel.Edit, typeof(GroupAuthMethod))]
        public async Task<ActionResult> PutGroup(int id, [FromBody] GroupInputModel input)
        {
            var group = await groupsRepo.GetAsync(id);

            group.Name = input.Name;
            group.HexColor = input.HexColor;

            if (await groupsRepo.SaveAsync(group))
                return Ok();
            return Problem();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthLevel.Edit, typeof(GroupAuthMethod))]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            if (await groupsRepo.DeleteAsync(id))
                return Ok();
            return Problem();
        }
    }
}
