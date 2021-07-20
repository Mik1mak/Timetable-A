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
    public class TimetableController : ControllerMyBase
    {
        private readonly ITimetableRepository timetablesRepo;
        private readonly IMapper mapper;

        public TimetableController(ITimetableRepository timetablesRepo, IMapper mapper, ILogger<TimetableController> logger)
        {
            this.timetablesRepo = timetablesRepo;
            this.mapper = mapper;
            timetablesRepo.Logger = logger;
        }

#if DEBUG == true
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Timetable>>> GetAll()
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
#endif

        [HttpGet]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<TimetableOutputModel>> Get()
        {
            var timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);

            var output = mapper.Map<TimetableOutputModel>(timetable);

            return Ok(output);
        }

        [HttpPost]
        public async Task<ActionResult<Timetable>> Post(TimetableInputModel input)
        {
            var newTimetable = mapper.Map<Timetable>(input);

            newTimetable.CreateDate = DateTime.Now;
            newTimetable.EditKey = KeyGen.Generate();
            newTimetable.ReadKey = KeyGen.Generate();

            if (await timetablesRepo.SaveAsync(newTimetable))
                return Ok(newTimetable);
                
            return Problem();
        }

        [HttpPut]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> Put([FromBody] TimetableInputModel input)
        {
            var timetable = await timetablesRepo.GetAsync(ThisTimetable.Id);

            timetable.Name = input.Name;
            timetable.Cycles = input.Cycles;

            if (await timetablesRepo.SaveAsync(timetable))
                return Ok();

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
