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
using TimetableA.API.Models.OutputModels;
using TimetableA.DataAccessLayer.Repositories.Abstract;

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
        public async Task<ActionResult<SimpleTimetableOutputModel>> Get()
        {
            var timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);

            return Ok(new SimpleTimetableOutputModel(timetable.Groups, this.settings));
        }

        [HttpGet("Week/{weekIndex}")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<WeekOutputModel>> GetWeek(int weekIndex)
        {
            var timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);

            return Ok(WeekOutputModel.GetWeekFromGroups(timetable.Groups, weekIndex));
        }
    }
}
