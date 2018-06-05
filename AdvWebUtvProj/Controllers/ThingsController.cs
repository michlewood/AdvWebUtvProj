using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvWebUtvProj.Data;
using AdvWebUtvProj.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvWebUtvProj.Controllers
{
    [Route("api/[controller]")]
    public class ThingsController : Controller
    { 
        private IThingsRepository _thingsRepository;

        public ThingsController(DatabaseContext context)
        {
            _thingsRepository = new ThingsRepository(context);
        }

        [HttpPost("seed")]
        public IActionResult Seed()
        {
            _thingsRepository.SeedRepo();
            return Ok("Things seeded");
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            int numberOfThings = _thingsRepository.Count();
            return Ok(numberOfThings);
        }

        [HttpPost]
        public IActionResult Add(Thing thing)
        {
            if (thing == null)
            {
                return BadRequest("News is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _thingsRepository.Add(thing);

            return Ok(thing.Id);
        }

        [HttpPut]
        public IActionResult Update(Models.UpdateThingModel thing)
        {
            if (thing == null)
            {
                return BadRequest("Thing is null");
            }

            if (!_thingsRepository.ThingExists(thing.Id))
            {
                return NotFound($"{thing.Id} not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            _thingsRepository.Update(thing);

            return NoContent();
        }

        [HttpDelete, Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _thingsRepository.Remove(id);
            return Ok(_thingsRepository.GetAll());
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_thingsRepository.GetAll());
        }
    }
}
