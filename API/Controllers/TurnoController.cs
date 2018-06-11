using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class TurnoController : Controller
    {
        private readonly IRepository<Turno> _repo;

        public TurnoController(IRepository<Turno> repo)
        {
            _repo = repo;
        }

        // GET: api/Turno
        [HttpGet]
        public IEnumerable<Turno> Get()
        {
            return _repo.GetAll();
        }

        // GET: api/Turno/5
        [HttpGet("{id}", Name = "GetTurno")]
        public IActionResult Get(int id)
        {
            var turno = _repo.GetById(id);
            if(turno == null)
            {
                return NotFound();
            }
            return Ok(turno);
        }
        
        // POST: api/Turno
        [HttpPost]
        public IActionResult Post([FromBody]Turno value)
        {
            if(value == null)
            {
                return BadRequest();
            }

            var turno = _repo.Add(value);
            return CreatedAtRoute("GetTurno", new { id = turno.IdTurno }, turno);
        }
        
        // PUT: api/Turno/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Turno value)
        {
            if(value == null)
            {
                return BadRequest();
            }

            var turno = _repo.GetById(id);
            if(turno == null)
            {
                return NotFound();
            }

            turno.IdTurno = id;
            _repo.Update(value);

            return NoContent();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var turno = _repo.GetById(id);
            if(turno == null)
            {
                return NotFound();
            }
            _repo.Delete(turno);
            return NoContent();
        }
    }
}
