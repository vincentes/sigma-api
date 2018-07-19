using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("[controller]")]
    public class MateriaController : Controller
    {
        private readonly IRepository<Materia> _repo;

        public MateriaController(IRepository<Materia> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<MateriaDto> Get()
        {
            IEnumerable<Materia> all = this._repo.GetAll();
            List<MateriaDto> materiaDtoList = new List<MateriaDto>();
            foreach (Materia materia in all)
                materiaDtoList.Add(DtoGet(materia));
            return materiaDtoList;
        }

        [HttpGet("{id}", Name = "GetMateria")]
        public IActionResult Get(int id)
        {
            Materia byId = this._repo.GetById(id);
            if (byId == null)
                return NotFound();
            return Ok(DtoGet(byId));
        }

        public MateriaDto DtoGet(Materia materia)
        {
            MateriaDto materiaDto = new MateriaDto()
            {
                Id = materia.Id,
                Nombre = materia.Nombre,
                Orientaciones = new List<OrientacionDto>()
            };
            foreach (MateriaOrientacion materiaOrientacion in materia.MateriaOrientacion)
            {
                OrientacionDto orientacionDto = new OrientacionDto()
                {
                    Id = materiaOrientacion.Orientacion.Id,
                    Nombre = materiaOrientacion.Orientacion.Nombre,
                    Grupos = new List<GrupoDto>()
                };
                foreach (Grupo grupo in materiaOrientacion.Orientacion.Grupos)
                    orientacionDto.Grupos.Add(new GrupoDto()
                    {
                        Id = grupo.Id,
                        Anio = grupo.Anio,
                        Grado = grupo.Grado,
                        TurnoId = grupo.TurnoId,
                        Turno = new TurnoDto()
                        {
                            Id = grupo.Turno.Id,
                            Nombre = grupo.Turno.Nombre
                        }
                    });
                materiaDto.Orientaciones.Add(orientacionDto);
            }
            return materiaDto;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Materia value)
        {
            if (value == null)
                return BadRequest();
            Materia materia = _repo.Add(value);
            return CreatedAtRoute("GetMateria", new
            {
                id = materia.Id
            }, materia);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Materia value)
        {
            if (value == null)
                return BadRequest();
            Materia byId = _repo.GetById(id);
            if (byId == null)
                return NotFound();
            byId.Id = id;
            _repo.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Materia byId = _repo.GetById(id);
            if (byId == null)
                return NotFound();
            _repo.Delete(byId);
            return NoContent();
        }

        public class MateriaDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }

            public int OrientacionId { get; set; }

            public List<OrientacionDto> Orientaciones { get; set; }
        }

        public class OrientacionDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }

            public virtual ICollection<GrupoDto> Grupos { get; set; }
        }

        public class GrupoDto
        {
            public int Id { get; set; }

            public int Grado { get; set; }

            public int Anio { get; set; }

            public int OrientacionId { get; set; }

            public int TurnoId { get; set; }

            public TurnoDto Turno { get; set; }
        }

        public class TurnoDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }
        }
    }
}
