using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public TimetableController(ITimetableRepository repository, IMapper mapper, ILogger<TimetableController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/<TimetableController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timetable>>> Get()
        {
            var timetable = await repository.GetAllBasicInfoAsync();

            if (timetable == null)
                return NotFound();
            if (!timetable.Any())
                return NotFound();

            //map to outputModel

            return Ok(timetable);
        }

        // GET api/<TimetableController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Timetable>> Get(int id)
        {
            var timetable = await repository.GetAsync(id);

            if (timetable == null)
                return NotFound();

            //map to outputModel

            return Ok(timetable);
        }

        // POST api/<TimetableController>
        [HttpPost]
        public async Task<ActionResult> Post(Timetable timetable)
        {
            if (timetable == null)
                return BadRequest("Timetable can't be null.");

            if (!ModelState.IsValid)
                return BadRequest();

            if (await repository.SaveAsync(timetable))
                return Ok();

            return Problem();
        }

        // PUT api/<TimetableController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Timetable timetable)
        {
            //inputModel
            timetable.Id = id;
            if (await repository.SaveAsync(timetable))
                return Ok();

            return Problem();
        }

        // DELETE api/<TimetableController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await repository.DeleteAsync(id))
                return Ok();

            return Problem();
        }
    }
}
