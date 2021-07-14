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
        private readonly ITimetableRepository repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public TimetableController(ITimetableRepository repository, IMapper mapper, ILogger<TimetableController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            repository.Logger = logger;
        }

        // GET: api/<TimetableController>
        [HttpGet]
        [Authorize(AuthLevel.Admin)]
        public async Task<ActionResult<IEnumerable<TimetableOutputModel>>> Get()
        {
            var timetable = await repository.GetAllAsync();

            if (timetable == null)
                return NotFound();
            if (!timetable.Any())
                return NotFound();


            var output = timetable.Select(x =>
            {
                x.Gropus?.Clear();
                return mapper.Map<TimetableOutputModel>(x);
            });

            return Ok(output);
        }

        // GET api/<TimetableController>/5
        [HttpGet("{id}")]
        [Authorize(AuthLevel.Read)]
        public async Task<ActionResult<TimetableOutputModel>> Get(int id)
        {
            var timetable = await repository.GetAsync(id);

            if (timetable == null)
                return NotFound();

            var output = mapper.Map<TimetableOutputModel>(timetable);

            return Ok(output);
        }

        // POST api/<TimetableController>
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

            if (await repository.SaveAsync(newTimetable))
                return Ok(newTimetable);
                

            return Problem();
        }

        // PUT api/<TimetableController>/5
        [HttpPut("{id}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> Put(int id, [FromBody] TimetableInputModel input)
        {
            var timetable = await repository.GetAsync(id);

            timetable.Name = input.Name;
            timetable.Cycles = input.Cycles;

            if (await repository.SaveAsync(timetable))
                return Ok();

            return Problem();
        }

        // DELETE api/<TimetableController>/5
        [HttpDelete("{id}")]
        [Authorize(AuthLevel.Edit)]
        public async Task<ActionResult> Delete(int id)
        {
            if (await repository.DeleteAsync(id))
                return Ok();

            return Problem();
        }
    }
}
