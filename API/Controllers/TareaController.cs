using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("[controller]")]
    public class TareaController : Controller
    {
        private readonly IRepository<Tarea> _repo;

        public TareaController(IRepository<Tarea> repo)
        {
            this._repo = repo;
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
                return (IActionResult)((ControllerBase)this).NotFound();
            return (IActionResult)((ControllerBase)this).Ok((object)this.DtoGet(byId));
        }

        public TareaController.TareaDto DtoGet(Tarea tarea)
        {
            TareaController.TareaDto tareaDto = new TareaController.TareaDto();
            tareaDto.Id = tarea.Id;
            tareaDto.DocenteId = tarea.DocenteId;
            tareaDto.MateriaId = tarea.MateriaId;
            tareaDto.Contenido = tarea.Contenido;
            tareaDto.ImageUrls = new List<string>();
            foreach (TareaImagen tareaImagen in tarea.TareaImagen)
                tareaDto.ImageUrls.Add(tareaImagen.Imagen.Url);
            return tareaDto;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tarea value)
        {
            if (value == null)
                return (IActionResult)((ControllerBase)this).BadRequest();
            Tarea tarea = this._repo.Add(value);
            return (IActionResult)((ControllerBase)this).CreatedAtRoute("GetTarea", (object)new
            {
                id = tarea.Id
            }, (object)tarea);
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

            public List<string> ImageUrls { get; set; }

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
