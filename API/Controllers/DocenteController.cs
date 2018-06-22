using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    public class DocenteController : Controller
    {
        private readonly IUserRepository<Docente> _repo;

        public DocenteController(IUserRepository<Docente> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Docente> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}", Name = "GetDocente")]
        public IActionResult Get(string id)
        {
            var docente = _repo.GetById(id);
            if (docente == null)
            {
                return NotFound();
            }
            return Ok(docente);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]DocenteUpdateDto value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var docente = _repo.GetById(id);
            if (docente == null)
            {
                return NotFound("Docente does not exist.");
            }

            docente.MateriaId = value.MateriaId;
            _repo.Update(docente);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class DocenteUpdateDto
    {
        public int MateriaId { get; set; }
    }
}
