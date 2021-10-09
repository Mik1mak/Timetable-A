using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;
using TimetableA.API.Services;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimetableA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerMyBase
    {
        private readonly ITimetableRepository timetablesRepo;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly AppSettings settings;

        public TimetableController(ITimetableRepository timetablesRepo, IAuthService authService,
            IMapper mapper, ILogger<TimetableController> logger, IOptions<AppSettings> settings)
        {
            this.settings = settings.Value;
            this.timetablesRepo = timetablesRepo;
            this.authService = authService;
            this.mapper = mapper;
            timetablesRepo.Logger = logger;
        }

#if DEBUG == true
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Timetable>>> GetAll()
        {
            IEnumerable<Timetable> timetable = await timetablesRepo.GetAllAsync();

            if (timetable == null)
                return NotFound();
            if (!timetable.Any())
                return NotFound();

            return Ok(timetable);
        }
#endif

        [HttpGet]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<TimetableOutputModel>> Get()
        {
            Timetable timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);
            TimetableOutputModel output = mapper.Map<TimetableOutputModel>(timetable);

            return Ok(output);
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> Post(TimetableInputModel input)
        {
            if (input.Cycles < 1 || input.Cycles > settings.MaxCyclesPerTimetable)
                return BadRequest($"Max count of weeks is {settings.MaxCyclesPerTimetable}");

            Timetable newTimetable = mapper.Map<Timetable>(input);

            newTimetable.CreateDate = DateTime.Now;
            newTimetable.EditKey = KeyGen.Generate();
            newTimetable.ReadKey = KeyGen.Generate();

            if (await timetablesRepo.SaveAsync(newTimetable))
            {
                var authRequest = new AuthenticateRequest { Id = newTimetable.Id, Key = newTimetable.EditKey };
                AuthenticateResponse response = await authService.Authenticate(authRequest);
                return Ok(response);
            }
                
                
            return Problem();
        }

        [HttpPut]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult<TimetableOutputModel>> Put([FromBody] TimetableInputModel input)
        {
            if (input.Cycles < 1 || input.Cycles > settings.MaxCyclesPerTimetable)
                return BadRequest($"Max count of weeks is {settings.MaxCyclesPerTimetable}");

            Timetable timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);

            timetable.Name = input.Name;
            timetable.Cycles = input.Cycles;
            timetable.DisplayEmptyDays = input.ShowWeekend;

            if (await timetablesRepo.SaveAsync(timetable))
            {
                TimetableOutputModel output = mapper.Map<TimetableOutputModel>(timetable);
                return Ok(output);
            }

            return Problem();
        }

        [HttpDelete]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> Delete()
        {
            if (await timetablesRepo.DeleteAsync(ThisTimetable.Id))
                return Ok();

            return Problem();
        }
    }
}
