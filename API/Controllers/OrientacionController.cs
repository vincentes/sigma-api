
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Docente")]
    public class OrientacionController : Controller
    {
        private readonly IRepository<Orientacion> _repo;

        public OrientacionController(IRepository<Orientacion> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Orientacion> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}", Name = "GetOrientacion")]
        public IActionResult Get(int id)
        {
            var orientacion = _repo.GetById(id);
            if (orientacion == null)
            {
                return NotFound();
            }
            return Ok(orientacion);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Orientacion value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var orientacion = _repo.Add(value);
            return CreatedAtRoute("GetOrientacion", new { id = orientacion.Id }, orientacion);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Orientacion value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var orientacion = _repo.GetById(id);
            if (orientacion == null)
            {
                return NotFound();
            }

            orientacion.Id = id;
            _repo.Update(value);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orientacion = _repo.GetById(id);
            if (orientacion == null)
            {
                return NotFound();
            }
            _repo.Delete(orientacion);
            return NoContent();
        }
    }
}
