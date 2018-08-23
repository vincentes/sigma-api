using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Repository
{
    [Route("[controller]/[action]")]
    public class GrupoController : Controller
    {
        private readonly IRepository<Grupo> _repo;
        private readonly IUserRepository<Alumno> _alumnos;

        public GrupoController(IRepository<Grupo> repo, IUserRepository<Alumno> alumnos)
        {
            _repo = repo;
            _alumnos = alumnos;
        }

        [HttpGet]
        public IEnumerable<GrupoController.GrupoDto> Get()
        {
            IEnumerable<Grupo> all = this._repo.GetAll();
            List<GrupoController.GrupoDto> grupoDtoList = new List<GrupoController.GrupoDto>();
            foreach (Grupo grupo in all)
                grupoDtoList.Add(this.DtoGet(grupo));
            return (IEnumerable<GrupoController.GrupoDto>)grupoDtoList;
        }

        [HttpGet("{id}", Name = "GetGrupo")]
        public IActionResult Get(int id)
        {
            Grupo byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            return (IActionResult)((ControllerBase)this).Ok((object)byId);
        }


        [HttpPost]
        public IActionResult AssignGrupo([FromBody] AssignGrupoToAlumnoDto dto)
        {
            Grupo byId = _repo.GetById(dto.GrupoId);
            if (byId == null)
                return NotFound();
            Alumno alumnoById = _alumnos.GetById(dto.AlumnoId);
            byId.Alumnos.Add(alumnoById);
            _repo.Update(byId);
            return Ok(byId);
        }

        public GrupoController.GrupoDto DtoGet(Grupo grupo)
        {
            GrupoController.GrupoDto grupoDto = new GrupoController.GrupoDto()
            {
                Id = grupo.Id,
                Anio = grupo.Anio,
                Grado = grupo.Grado,
                Orientacion = new OrientacionDto()
                {
                    Id = grupo.Orientacion.Id,
                    Nombre = grupo.Orientacion.Nombre
                },
                Turno = new TurnoDto()
                {
                    Id = grupo.Turno.Id,
                    Nombre = grupo.Turno.Nombre
                },
                TurnoId = grupo.TurnoId
            };
            grupoDto.Docentes = new List<Docente>();
            foreach (GrupoDocente grupoDocente in (IEnumerable<GrupoDocente>)grupo.GrupoDocentes)
                grupoDto.Docentes.Add(grupoDocente.Docente);
            return grupoDto;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Grupo value)
        {
            if (value == null)
                return (IActionResult)((ControllerBase)this).BadRequest();
            Grupo grupo = this._repo.Add(value);
            return (IActionResult)((ControllerBase)this).CreatedAtRoute("GetGrupo", (object)new
            {
                id = grupo.Id
            }, (object)grupo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Grupo value)
        {
            if (value == null)
                return (IActionResult)((ControllerBase)this).BadRequest();
            Grupo byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            byId.Id = id;
            this._repo.Update(value);
            return (IActionResult)((ControllerBase)this).NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Grupo byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            this._repo.Delete(byId);
            return (IActionResult)((ControllerBase)this).NoContent();
        }

        public class GrupoDto
        {
            public int Id { get; set; }

            public int Grado { get; set; }

            public int Anio { get; set; }

            public int TurnoId { get; set; }

            public GrupoController.TurnoDto Turno { get; set; }

            public GrupoController.OrientacionDto Orientacion { get; set; }

            public List<Docente> Docentes { get; set; }
        }

        public class OrientacionDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }
        }

        public class TurnoDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }
        }
        public class AssignGrupoToAlumnoDto
        {
            public int GrupoId { get; set; }
            public string AlumnoId { get; set; }
        }
    }

}
