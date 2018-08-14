using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]/[action]")]
    public class DocenteController : Controller
    {
        private readonly IUserRepository<Docente> _repo;
        private readonly IRepository<GrupoDocente> _gd;
        private readonly UserManager<AppUser> _userManager;

        public DocenteController(IUserRepository<Docente> repo, IRepository<GrupoDocente> gd, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _gd = gd;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<Docente> Get()
        {
            return _repo.GetAll();
        }

        [Authorize(Roles = "Docente")]
        public IActionResult GetInfo()
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser appUser = _userManager.Users.SingleOrDefault(r => r.Id == userID);
            Docente docente = _repo.GetById(appUser.Id);
            DocenteGetDto result = new DocenteGetDto();
            result.Grupos = new List<GrupoDto>();
            foreach (GrupoDocente gd in docente.GrupoDocentes)
            {
                result.Grupos.Add(new GrupoDto
                {
                    Id = gd.Grupo.Id,
                    Anio = gd.Grupo.Anio,
                    Grado = gd.Grupo.Grado,
                    Numero = gd.Grupo.Numero,
                    OrientacionId = gd.Grupo.OrientacionId,
                    TurnoId = gd.Grupo.TurnoId
                });
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult AssignGrupo([FromBody] AssignGrupoDto agd)
        {
            Docente docente = _repo.GetById(agd.DocenteId);
            if(docente == null)
            {
                return NotFound();
            }

            GrupoDocente gd = new GrupoDocente
            {
                Docente = docente,
                GrupoId = agd.GrupoId
            };
            _gd.Add(gd);

            docente.GrupoDocentes.Add(gd);
            _repo.Update(docente);
            return Ok();
        }

        [HttpGet("{id}", Name = "GetDocente")]
        public IActionResult Get(string id)
        {
            var docente = _repo.GetById(id);
            if (docente == null)
            {
                return NotFound();
            }

            DocenteGetDto result = new DocenteGetDto();
            foreach(GrupoDocente gd in docente.GrupoDocentes)
            {
                result.Grupos.Add(new GrupoDto
                {
                    Id = gd.Grupo.Id,
                    Anio = gd.Grupo.Anio,
                    Grado = gd.Grupo.Grado,
                    Numero = gd.Grupo.Numero,
                    OrientacionId = gd.Grupo.OrientacionId,
                    TurnoId = gd.Grupo.TurnoId
                });
            }
            return Ok(result);
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

    public class AssignGrupoDto
    {
        public string DocenteId { get; set; }
        public int GrupoId { get; set; }
    }

    public class DocenteGetDto
    {
        public List<GrupoDto> Grupos { get; set; }
    }

    public class GrupoDto
    {
        public int Id { get; set; }
        public int Grado { get; set; }
        public int Numero { get; set; }
        public int Anio { get; set; }
        public int OrientacionId { get; set; }
        public int TurnoId { get; set; }
    }

    public class DocenteUpdateDto
    {
        public int MateriaId { get; set; }
    }
}
