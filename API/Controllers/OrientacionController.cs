// Decompiled with JetBrains decompiler
// Type: API.Controllers.OrientacionController
// Assembly: API, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B418147-8FFB-41A2-8EEF-9BE2FCA642AC
// Assembly location: C:\Users\micro\Documents\decompiling\API.dll

using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("Orientacion")]
    public class OrientacionController : Controller
    {
        private readonly IRepository<Orientacion> _repo;

        public OrientacionController(IRepository<Orientacion> repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public IEnumerable<OrientacionDto> Get()
        {
            IEnumerable<Orientacion> all = this._repo.GetAll();
            List<OrientacionDto> orientacionDtoList = new List<OrientacionController.OrientacionDto>();
            foreach (Orientacion orientacion in all)
                orientacionDtoList.Add(this.DtoGet(orientacion));
            return (IEnumerable<OrientacionController.OrientacionDto>)orientacionDtoList;
        }

        [HttpGet("{id}", Name = "GetOrientacion")]
        public IActionResult Get(int id)
        {
            Orientacion byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            return (IActionResult)((ControllerBase)this).Ok((object)this.DtoGet(byId));
        }

        public OrientacionController.OrientacionDto DtoGet(Orientacion orientacion)
        {
            List<OrientacionController.MateriaDto> materiaDtoList = new List<OrientacionController.MateriaDto>();
            foreach (MateriaOrientacion materiaOrientacion in (IEnumerable<MateriaOrientacion>)orientacion.MateriaOrientacion)
                materiaDtoList.Add(new OrientacionController.MateriaDto()
                {
                    Id = materiaOrientacion.Materia.Id,
                    Nombre = materiaOrientacion.Materia.Nombre
                });
            List<OrientacionController.GrupoDto> grupoDtoList = new List<OrientacionController.GrupoDto>();
            foreach (Grupo grupo in (IEnumerable<Grupo>)orientacion.Grupos)
                grupoDtoList.Add(new OrientacionController.GrupoDto()
                {
                    Id = grupo.Id,
                    Anio = grupo.Anio,
                    Grado = grupo.Grado,
                    TurnoId = grupo.TurnoId,
                    Turno = new OrientacionController.TurnoDto()
                    {
                        Id = grupo.Turno.Id,
                        Nombre = grupo.Turno.Nombre
                    }
                });
            return new OrientacionController.OrientacionDto()
            {
                Id = orientacion.Id,
                Nombre = orientacion.Nombre,
                Materias = (ICollection<OrientacionController.MateriaDto>)materiaDtoList,
                Grupos = (ICollection<OrientacionController.GrupoDto>)grupoDtoList
            };
        }

        [HttpPost]
        public IActionResult Post([FromBody] Orientacion value)
        {
            if (value == null)
                return (IActionResult)((ControllerBase)this).BadRequest();
            Orientacion orientacion = this._repo.Add(value);
            return (IActionResult)((ControllerBase)this).CreatedAtRoute("GetOrientacion", (object)new
            {
                id = orientacion.Id
            }, (object)orientacion);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Orientacion value)
        {
            if (value == null)
                return (IActionResult)((ControllerBase)this).BadRequest();
            Orientacion byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            byId.Id = id;
            this._repo.Update(value);
            return (IActionResult)((ControllerBase)this).NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Orientacion byId = this._repo.GetById(id);
            if (byId == null)
                return (IActionResult)((ControllerBase)this).NotFound();
            this._repo.Delete(byId);
            return (IActionResult)((ControllerBase)this).NoContent();
        }

        public class MateriaDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }
        }

        public class OrientacionDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }

            public ICollection<OrientacionController.MateriaDto> Materias { get; set; }

            public ICollection<OrientacionController.GrupoDto> Grupos { get; set; }
        }

        public class GrupoDto
        {
            public int Id { get; set; }

            public int Grado { get; set; }

            public int Anio { get; set; }

            public int OrientacionId { get; set; }

            public int TurnoId { get; set; }

            public OrientacionController.TurnoDto Turno { get; set; }
        }

        public class TurnoDto
        {
            public int Id { get; set; }

            public string Nombre { get; set; }
        }
    }
}
