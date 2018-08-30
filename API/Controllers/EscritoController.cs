using API.Models;
using API.Repository;
using API.Utils;
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
    [Authorize(Roles = "Docente, Alumno")]
    public class EscritoController : Controller
    {
        private readonly IRepository<Escrito> _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Grupo> _grupos;

        public EscritoController(IRepository<Escrito> repo, IRepository<Grupo> grupos, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
            _grupos = grupos;
        }

        [HttpGet]
        public IEnumerable<GetEscritoDto> Get()
        {
            IEnumerable<Escrito> all = this._repo.GetAll();
            List<GetEscritoDto> escritoList = new List<GetEscritoDto>();
            foreach (Escrito escrito in all)
                escritoList.Add(DtoGet(escrito));
            return escritoList;
        }


        [HttpGet("{id}", Name = "GetEscrito")]
        public IActionResult Get(int id)
        {
            Escrito byId = _repo.GetById(id);
            if (byId == null)
                return NotFound();
            return Ok(DtoGet(byId));
        }

        public GetEscritoDto DtoGet(Escrito escrito)
        {
            GetEscritoDto escritoDto = new GetEscritoDto
            {
                Id = escrito.Id,
                DocenteId = escrito.DocenteId,
                MateriaId = escrito.MateriaId,
                Temas = escrito.Temas,
                Fecha = escrito.GruposAsignados.ElementAt(0).Date,
                GruposAsignados = new List<GrupoDto>()
            };

            foreach(EscritoGrupo grupo in escrito.GruposAsignados)
            {
                escritoDto.GruposAsignados.Add(new GrupoDto
                {
                    Id = grupo.Grupo.Id,
                    Anio = grupo.Grupo.Anio,
                    Grado = grupo.Grupo.Grado,
                    Numero = grupo.Grupo.Numero
                });
            }

            return escritoDto;
        }

        [HttpGet]
        public IEnumerable<GetEscritoDto> GetDocenteEscritos()
        {
            IEnumerable<Escrito> all = this._repo.GetAll();
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser user = _userManager.Users.SingleOrDefault(r => r.Id == userID);

            List<GetEscritoDto> output = new List<GetEscritoDto>();
            foreach(Escrito escrito in all)
            {
                if(escrito.DocenteId == user.Id)
                {
                    output.Add(DtoGet(escrito));
                }
            }

            return output;
        }

        [HttpGet]
        public IEnumerable<GetEscritoDto> GetAlumnoEscritos()
        {
            IEnumerable<Escrito> all = this._repo.GetAll();
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Alumno user = (Alumno) _userManager.Users.SingleOrDefault(r => r.Id == userID);

            List<GetEscritoDto> output = new List<GetEscritoDto>();
            foreach (Escrito escrito in all)
            {
                foreach(EscritoGrupo pg in escrito.GruposAsignados)
                {
                    if(pg.GrupoId == user.GrupoId)
                    {
                       output.Add(DtoGet(escrito));
                    }
                }
            }
            return output;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostEscritoDto escrito)
        {
            if (escrito == null)
                return BadRequest();

            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser user = _userManager.Users.SingleOrDefault(r => r.Id == userID);

            Escrito escritoObject = new Escrito
            {
                Temas = escrito.Temas,
                DocenteId = user.Id,
                MateriaId = escrito.MateriaId,
                GruposAsignados = new List<EventoGrupo>()
            };

            foreach(GrupoDto grupoAsignado in escrito.GruposAsignados)
            {
                Grupo grupo = _grupos.GetById(grupoAsignado.Id);
                escritoObject.GruposAsignados.Add(new EscritoGrupo
                {
                    Date = escrito.Date,
                    GrupoId = grupoAsignado.Id,
                    Evento = escritoObject
                });
            }

            Escrito addEscrito = _repo.Add(escritoObject);
            Firebase.NotifyCreated(addEscrito);
            return CreatedAtRoute("GetEscrito", new
            {
                id = addEscrito.Id
            }, new
            {
                Escrito = DtoGet(addEscrito)
            });
        }

        public class PostEscritoDto
        {
            public int Id { get; set; }
            public int MateriaId { get; set; }
            public string Temas { get; set; }
            public DateTime Date { get; set; }
            public List<GrupoDto> GruposAsignados { get; set; }
        }

        public class EscritoDto
        {
            public int Id { get; set; }
            public string DocenteId { get; set; }
            public int MateriaId { get; set; }
            public string Temas { get; set; }
            public List<GrupoDto> GruposAsignados { get; set; }
        }

        public class GetEscritoDto
        {
            public int Id { get; set; }
            public DateTime Fecha { get; set; }
            public string DocenteId { get; set; }
            public int MateriaId { get; set; }
            public string Temas { get; set; }
            public List<GrupoDto> GruposAsignados { get; set; }
        }

        public class GrupoDto
        {
            public int Id { get; set; }

            public int Grado { get; set; }
            public int Numero { get; set; }

            public int Anio { get; set; }
            public DateTime Fecha { get; set; }
        }
    }

}
