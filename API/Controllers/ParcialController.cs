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
    public class ParcialController : Controller
    {
        private readonly IRepository<Parcial> _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Grupo> _grupos;
        private readonly IRepository<ParcialGrupo> _parciales;

        public ParcialController(IRepository<Parcial> repo, IRepository<ParcialGrupo> parciales, IRepository<Grupo> grupos, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
            _grupos = grupos;
            _parciales = parciales;
        }

        [HttpGet]
        public IEnumerable<GetParcialDto> Get()
        {
            IEnumerable<Parcial> all = this._repo.GetAll();
            List<GetParcialDto> parcialList = new List<GetParcialDto>();
            foreach (Parcial parcial in all)
                parcialList.Add(DtoGet(parcial));
            return parcialList;
        }


        [HttpGet("{id}", Name = "GetParcial")]
        public IActionResult Get(int id)
        {
            Parcial byId = _repo.GetById(id);
            if (byId == null)
                return NotFound();
            return Ok(DtoGet(byId));
        }

        public GetParcialDto DtoGet(Parcial parcial)
        {
            GetParcialDto parcialDto = new GetParcialDto
            {
                Id = parcial.Id,
                DocenteId = parcial.DocenteId,
                MateriaId = parcial.MateriaId,
                Temas = parcial.Temas,
                Fecha = parcial.GruposAsignados.ElementAt(0).Date,
                GruposAsignados = new List<GrupoDto>()
            };

            foreach(ParcialGrupo grupo in parcial.GruposAsignados)
            {
                parcialDto.GruposAsignados.Add(new GrupoDto
                {
                    Id = grupo.Grupo.Id,
                    Anio = grupo.Grupo.Anio,
                    Grado = grupo.Grupo.Grado,
                    Numero = grupo.Grupo.Numero
                });
            }

            return parcialDto;
        }

        [HttpGet]
        public IEnumerable<GetParcialDto> GetDocenteParciales()
        {
            IEnumerable<Parcial> all = this._repo.GetAll();
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser user = _userManager.Users.SingleOrDefault(r => r.Id == userID);

            List<GetParcialDto> output = new List<GetParcialDto>();
            foreach(Parcial parcial in all)
            {
                if(parcial.DocenteId == user.Id)
                {
                    RParcialGrupo pg = (RParcialGrupo)_parciales;
                    List<ParcialGrupo> gruposAsignados = pg.GetAssignedGroups(parcial.Id);
                    parcial.GruposAsignados = gruposAsignados;
                    output.Add(DtoGet(parcial));
                }
            }

            return output;
        }

        [HttpGet]
        public IEnumerable<GetParcialDto> GetAlumnoParciales()
        {
            IEnumerable<Parcial> all = this._repo.GetAll();
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Alumno user = (Alumno) _userManager.Users.SingleOrDefault(r => r.Id == userID);

            List<GetParcialDto> output = new List<GetParcialDto>();
            foreach (Parcial parcial in all)
            {
                foreach(ParcialGrupo pg in parcial.GruposAsignados)
                {
                    if(pg.GrupoId == user.GrupoId)
                    {
                       output.Add(DtoGet(parcial));
                    }
                }
            }
            return output;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostParcialDto parcial)
        {
            if (parcial == null)
                return BadRequest();

            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser user = _userManager.Users.SingleOrDefault(r => r.Id == userID);

            Parcial parcialObject = new Parcial
            {
                Temas = parcial.Temas,
                DocenteId = user.Id,
                MateriaId = parcial.MateriaId,
                GruposAsignados = new List<ParcialGrupo>()
            };

            foreach(GrupoDto grupoAsignado in parcial.GruposAsignados)
            {
                Grupo grupo = _grupos.GetById(grupoAsignado.Id);
                parcialObject.GruposAsignados.Add(new ParcialGrupo
                {
                    Date = parcial.Date,
                    GrupoId = grupoAsignado.Id,
                    Parcial = parcialObject
                });
            }

            Parcial addParcial = _repo.Add(parcialObject);
            Firebase.NotifyCreated(addParcial);
            return CreatedAtRoute("GetParcial", new
            {
                id = addParcial.Id
            }, new
            {
                Parcial = DtoGet(addParcial)
            });
        }

        public class PostParcialDto
        {
            public int Id { get; set; }
            public int MateriaId { get; set; }
            public string Temas { get; set; }
            public DateTime Date { get; set; }
            public List<GrupoDto> GruposAsignados { get; set; }
        }

        public class ParcialDto
        {
            public int Id { get; set; }
            public string DocenteId { get; set; }
            public int MateriaId { get; set; }
            public string Temas { get; set; }
            public List<GrupoDto> GruposAsignados { get; set; }
        }

        public class GetParcialDto
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
