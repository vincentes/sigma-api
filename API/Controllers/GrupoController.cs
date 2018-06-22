using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Repository
{
    [Route("[controller]")]
    public class GrupoController : Controller
    {
        private readonly IRepository<Grupo> _repo;

        public GrupoController(IRepository<Grupo> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IEnumerable<Grupo> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}", Name = "GetGrupo")]
        public IActionResult Get(int id)
        {
            var grupo = _repo.GetById(id);
            if (grupo == null)
            {
                return NotFound();
            }
            return Ok(grupo);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Grupo value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var grupo = _repo.Add(value);
            return CreatedAtRoute("GetGrupo", new { id = grupo.Id }, grupo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Grupo value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var grupo = _repo.GetById(id);
            if (grupo == null)
            {
                return NotFound();
            }

            grupo.Id = id;
            _repo.Update(value);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var grupo = _repo.GetById(id);
            if (grupo == null)
            {
                return NotFound();
            }
            _repo.Delete(grupo);
            return NoContent();
        }
    
    }
}
