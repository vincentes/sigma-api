using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("[controller]/[action]")]
    public class TareaController : Controller
    {
        private readonly IRepository<Tarea> _repo;
        private readonly IRepository<TareaGrupo> _assignments;
        private readonly IRepository<Grupo> _grupos;
        private readonly UserManager<AppUser> _userManager;
        public TareaController(IRepository<Tarea> repo, IRepository<Grupo> grupos, IRepository<TareaGrupo> assignments,  UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
            _assignments = assignments;
            _grupos = grupos;
        }

        [HttpGet]
        public IEnumerable<TareaDto> Get()
        {
            IEnumerable<Tarea> all = this._repo.GetAll();
            List<TareaDto> tareaDtoList = new List<TareaDto>();
            foreach (Tarea tarea in all)
                tareaDtoList.Add(this.DtoGet(tarea));
            return tareaDtoList;
        }

        [HttpPut]
        [Authorize(Roles = "Docente")]
        public IActionResult AssignGrupo([FromBody] AssignGrupoDto agd)
        {
            if(agd == null)
            {
                return BadRequest();
            }
            List<Grupo> grupos = new List<Grupo>();
            foreach(int grupoId in agd.GrupoIds)
            {
                grupos.Add(_grupos.GetById(grupoId));
            }

            Tarea tarea = _repo.GetById(agd.TareaId);
            if(tarea == null)
            {
                return NotFound();
            }
            
            foreach(Grupo grupo in grupos)
            {
                _assignments.Add(new TareaGrupo
                {
                    Deadline = agd.Deadline,
                    Grupo = grupo,
                    Tarea = tarea
                });
            }
            return Ok();
        }

        [HttpGet("{id}", Name = "GetTarea")]
        public IActionResult Get(int id)
        {
            Tarea byId = _repo.GetById(id);
            if (byId == null)
                return NotFound();
            return Ok(DtoGet(byId));
        }

        [HttpGet("{id}", Name = "GetAssignedGrupos")]
        public IActionResult GetAssignedGrupos(int id)
        {
            Tarea byId = _repo.GetById(id);
            if (byId == null)
                return NotFound();
            GetAssignedGruposDto assignments = new GetAssignedGruposDto();
            assignments.Assignments = new List<AssignmentDto>();
            foreach (TareaGrupo tg in byId.TareaGrupos)
            {
                assignments.Assignments.Add(new AssignmentDto
                {
                    Deadline = tg.Deadline,
                    GroupId = tg.GrupoId
                });
            }
            return Ok(assignments);
        }

        public TareaDto DtoGet(Tarea tarea)
        {
            TareaDto tareaDto = new TareaDto
            {
                Id = tarea.Id,
                DocenteId = tarea.DocenteId,
                MateriaId = tarea.MateriaId,
                Contenido = tarea.Contenido,
                ImageIds = new List<int>()
            };
            foreach (TareaImagen tareaImagen in tarea.TareaImagen)
                tareaDto.ImageIds.Add(tareaImagen.ImagenId);
            return tareaDto;
        }

        [Authorize(Roles = "Docente")]
        [HttpPost]
        public IActionResult Post([FromBody] PostTareaDto value)
        {
            if (value == null)
                return BadRequest();

            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser user = _userManager.Users.SingleOrDefault(r => r.Id == userID);

            Tarea tarea = new Tarea()
            {
                Contenido = value.Contenido,
                Docente = (Docente) user,
                MateriaId = value.MateriaId
            };

            List<TareaImagen> imagenes = new List<TareaImagen>();
            foreach(int id in value.ImageIds)
            {
                TareaImagen tareaImagen= new TareaImagen()
                {
                    ImagenId = id,
                    Tarea = tarea
                };

                imagenes.Add(tareaImagen);
            }

            tarea.TareaImagen = imagenes;
            Tarea addTarea = _repo.Add(tarea);
            return CreatedAtRoute("GetTarea", new
            {
                id = addTarea.Id
            }, new
            {
                Tarea = DtoGet(addTarea)
            });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tarea value)
        {
            if (value == null)
                return (IActionResult)((ControllerBase)this).BadRequest();
            Tarea byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            byId.Id = id;
            this._repo.Update(value);
            return (IActionResult)((ControllerBase)this).NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Tarea byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            this._repo.Delete(byId);
            return (IActionResult)((ControllerBase)this).NoContent();
        }

        public class GetAssignedGruposDto
        {
            public List<AssignmentDto> Assignments { get; set; }
        }

        public class AssignmentDto
        {
            public DateTime Deadline { get; set; }
            public int GroupId { get; set; }
        }

        public class AssignGrupoDto
        {
            public DateTime Deadline { get; set; }
            public List<int> GrupoIds { get; set; }
            public int TareaId { get; set; }
        }

        public class TareaDto
        {
            public int Id { get; set; }

            public List<int> ImageIds { get; set; }

            public string DocenteId { get; set; }

            public int MateriaId { get; set; }

            public string Contenido { get; set; }
        }

        public class PostTareaDto
        {
            public List<int> ImageIds { get; set; }

            public int MateriaId { get; set; }

            public string Contenido { get; set; }
        }

        public class MateriaDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }

            public int OrientacionId { get; set; }

            public List<TareaController.OrientacionDto> Orientaciones { get; set; }
        }

        public class OrientacionDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }

            public virtual ICollection<TareaController.GrupoDto> Grupos { get; set; }
        }

        public class GrupoDto
        {
            public int Id { get; set; }

            public int Grado { get; set; }

            public int Anio { get; set; }

            public int OrientacionId { get; set; }

            public int TurnoId { get; set; }

            public TareaController.TurnoDto Turno { get; set; }
        }

        public class TurnoDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }
        }
    }
}
