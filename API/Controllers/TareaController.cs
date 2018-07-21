﻿using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("[controller]")]
    public class TareaController : Controller
    {
        private readonly IRepository<Imagen> _repoImage;
        private readonly IRepository<TareaImagen> _repoTI;
        private readonly IRepository<Tarea> _repo;
        private readonly UserManager<AppUser> _userManager;

        public TareaController(IRepository<Tarea> repo, IRepository<Imagen> repoImage, IRepository<TareaImagen> repoTI,  UserManager<AppUser> userManager)
        {
            _repo = repo;
            _repoImage = repoImage;
            _repoTI = repoTI;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<TareaController.TareaDto> Get()
        {
            IEnumerable<Tarea> all = this._repo.GetAll();
            List<TareaController.TareaDto> tareaDtoList = new List<TareaController.TareaDto>();
            foreach (Tarea tarea in all)
                tareaDtoList.Add(this.DtoGet(tarea));
            return (IEnumerable<TareaController.TareaDto>)tareaDtoList;
        }

        [HttpGet("{id}", Name = "GetTarea")]
        public IActionResult Get(int id)
        {
            Tarea byId = this._repo.GetById(id);
            if (byId == null)
                return NotFound();
            return Ok(DtoGet(byId));
        }

        public TareaDto DtoGet(Tarea tarea)
        {
            TareaDto tareaDto = new TareaDto();
            tareaDto.Id = tarea.Id;
            tareaDto.DocenteId = tarea.DocenteId;
            tareaDto.MateriaId = tarea.MateriaId;
            tareaDto.Contenido = tarea.Contenido;
            tareaDto.ImageIds = new List<int>();
            foreach (TareaImagen tareaImagen in tarea.TareaImagen)
                tareaDto.ImageIds.Add(tareaImagen.Imagen.Id);
            return tareaDto;
        }

        [Authorize(Roles = "Docente")]
        [HttpPost]
        public IActionResult Post([FromBody] TareaDto value)
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
            }

            tarea.TareaImagen = imagenes;
            _repo.Add(tarea);
            return CreatedAtRoute("GetTarea", (object)new
            {
                id = tarea.Id
            }, tarea);
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

        public class TareaDto
        {
            public int Id { get; set; }
            public List<int> ImageIds { get; set; }

            public string DocenteId { get; set; }

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
