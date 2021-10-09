using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.API.DTO.OutputModels;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

namespace TimetableA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AltTimetableController : ControllerMyBase
    {
        private readonly ITimetableRepository timetablesRepo;
        private readonly AppSettings settings;

        public AltTimetableController(
            ITimetableRepository timetablesRepo,
            ILogger<AltTimetableController> logger,
            IOptions<AppSettings> settings)
        {
            this.settings = settings.Value;
            this.timetablesRepo = timetablesRepo;
            timetablesRepo.Logger = logger;
        }

        [HttpGet]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<AltTimetableOutputModel>> Get()
        {
            Timetable timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);

            AltTimetableOutputModel output = AltTimetableOutputModelFactory.FromGroups(timetable.Groups, this.settings);

            return Ok(output);
        }

        [HttpGet("Week/{weekIndex}")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<WeekOutputModel>> GetWeek(int weekIndex)
        {
            Timetable timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);

            WeekOutputModel output = WeekOutputModelFactory.FromGroups(timetable.Groups, weekIndex);

            return Ok(output);
        }
    }
}
