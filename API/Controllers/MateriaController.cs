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
    public class MateriaController : Controller
    {
        private readonly IRepository<Materia> _repo;

        public MateriaController(IRepository<Materia> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Materia> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}", Name = "GetMateria")]
        public IActionResult Get(int id)
        {
            var materia = _repo.GetById(id);
            if (materia == null)
            {
                return NotFound();
            }
            return Ok(materia);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Materia value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var materia = _repo.Add(value);
            return CreatedAtRoute("GetMateria", new { id = materia.Id }, materia);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Materia value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var materia = _repo.GetById(id);
            if (materia == null)
            {
                return NotFound();
            }

            materia.Id = id;
            _repo.Update(value);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var materia = _repo.GetById(id);
            if (materia == null)
            {
                return NotFound();
            }
            _repo.Delete(materia);
            return NoContent();
        }

    }
}