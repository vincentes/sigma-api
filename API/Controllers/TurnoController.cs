using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("[controller]")]
    public class TurnoController : Controller
    {
        private readonly IRepository<Turno> _repo;

        public TurnoController(IRepository<Turno> repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public IEnumerable<Turno> Get()
        {
            return this._repo.GetAll();
        }

        [HttpGet("{id}", Name = "GetTurno")]
        public IActionResult Get(int id)
        {
            Turno byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            return (IActionResult)((ControllerBase)this).Ok((object)byId);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Turno value)
        {
            if (value == null)
                return (IActionResult)((ControllerBase)this).BadRequest();
            Turno turno = this._repo.Add(value);
            return (IActionResult)((ControllerBase)this).CreatedAtRoute("GetTurno", (object)new
            {
                id = turno.Id
            }, (object)turno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Turno value)
        {
            if (value == null)
                return (IActionResult)((ControllerBase)this).BadRequest();
            Turno byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            byId.Id = id;
            this._repo.Update(value);
            return (IActionResult)((ControllerBase)this).NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Turno byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            this._repo.Delete(byId);
            return (IActionResult)((ControllerBase)this).NoContent();
        }

        public class TurnoDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }

            public List<TurnoController.GrupoDto> Grupos { get; set; }
        }

        public class GrupoDto
        {
            public int Id { get; set; }

            public int Grado { get; set; }

            public int Anio { get; set; }

            public int OrientacionId { get; set; }

            public int TurnoId { get; set; }

            public TurnoController.OrientacionDto Orientacion { get; set; }
        }

        public class OrientacionDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }
        }
    }
}
