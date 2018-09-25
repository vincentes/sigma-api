using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static API.Controllers.MateriaController;

namespace API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("[controller]/[action]")]
    public class CambioDeSalonController : Controller
    {
        private readonly IRepository<CambioDeSalon> _repo;
        private readonly IRepository<Pregunta> _preguntas;
        private readonly IRepository<PreguntaOpcion> _opciones;
        private readonly IRepository<Salon> _salones;
        private readonly IRepository<HoraMateria> _horas;
        private readonly IRepository<Turno> _turnos;
        private readonly IUserRepository<Adscripto> _adscriptos;
        private readonly IUserRepository<Alumno> _alumnos;
        private readonly UserManager<AppUser> _userManager;

        public CambioDeSalonController(IRepository<CambioDeSalon> repo, IRepository<Turno> turnos, IRepository<HoraMateria> horas, IRepository<Salon> salones, IUserRepository<Adscripto> adscriptos, IUserRepository<Alumno> alumnos, UserManager<AppUser> userManager)
        {
            this._repo = repo;
            this._adscriptos = adscriptos;
            this._alumnos = alumnos;
            this._userManager = userManager;
            this._salones = salones;
            this._horas = horas;
            this._turnos = turnos;
        }

        [HttpGet("{id}", Name = "GetCambioDeSalon")]
        public IActionResult Get(int id)
        {
            CambioDeSalon byId = _repo.GetById(id);
            if (byId == null)
            {
                return NotFound();
            }

            return Ok(DtoGet(byId));
        }


        [HttpGet]
        public IActionResult GetInfo()
        {
            List<CSGetHM> hms = new List<CSGetHM>();
            foreach(HoraMateria hm in _horas.GetAll())
            {
                hms.Add(new CSGetHM
                {
                    Dia = hm.Dia,
                    Grupo = new GrupoDto
                    {
                        Id = hm.GrupoId,
                        Anio = hm.Grupo.Anio,
                        Grado = hm.Grupo.Grado,
                        Numero = hm.Grupo.Numero,
                        OrientacionId = hm.Grupo.OrientacionId,
                        Turno = hm.Grupo.Turno.Nombre,
                    },
                    Hora = hm.Hora,
                    Salon = new CSGetSalon
                    {
                        Id = hm.Salon.Id,
                        Nombre = hm.Salon.Nombre
                    }
                });
            }

            return Ok(hms);
        }

        public CSGet DtoGet(CambioDeSalon cs)
        {
            var turno = _turnos.GetById(cs.HoraMateriaInicial.Grupo.TurnoId);
            CSGet dto = new CSGet
            {
                Fecha = cs.Fecha,
                SalonFinal = new CSGetSalon
                {
                    Id = cs.SalonFinalId,
                    Nombre = cs.SalonFinal.Nombre
                },
                SalonInicial = new CSGetSalon
                {
                    Id = cs.SalonInicialId,
                    Nombre = cs.SalonFinal.Nombre
                },
                HoraMateriaInicial = new CSGetHM
                {
                    Id = cs.HoraMateriaInicialId,
                    Dia = cs.HoraMateriaInicial.Dia,
                    Grupo = new GrupoDto
                    {
                        Id = cs.HoraMateriaInicial.GrupoId,
                        Anio = cs.HoraMateriaInicial.Grupo.Anio,
                        Grado = cs.HoraMateriaInicial.Grupo.Grado,
                        Numero = cs.HoraMateriaInicial.Grupo.Numero,
                        OrientacionId = cs.HoraMateriaInicial.Grupo.OrientacionId,
                        Turno = turno.Nombre
                    }
                },
                HoraMateriaFinal = new CSGetHM
                {
                    Id = cs.HoraMateriaFinalId,
                    Dia = cs.HoraMateriaFinal.Dia,
                    Grupo = new GrupoDto
                    {
                        Id = cs.HoraMateriaFinal.GrupoId,
                        Anio = cs.HoraMateriaFinal.Grupo.Anio,
                        Grado = cs.HoraMateriaFinal.Grupo.Grado,
                        Numero = cs.HoraMateriaFinal.Grupo.Numero,
                        OrientacionId = cs.HoraMateriaFinal.Grupo.OrientacionId,
                        Turno = turno.Nombre
                    }
                },
                SalonDestino = new CSGetSalon
                {
                    Id = cs.SalonCambioId,
                    Nombre = cs.SalonCambio.Nombre
                }
            };

            return dto;
        }

        public class CSGet
        {
            public DateTime Fecha { get; set; }
            public CSGetSalon SalonInicial { get; set; }
            public CSGetSalon SalonDestino { get; set; }
            public CSGetHM HoraMateriaInicial { get; set; }
            public CSGetHM HoraMateriaFinal { get; set; }
            public CSGetSalon SalonFinal { get; set; }
        }

        public class CSGetSalon
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }

        public class CSGetGrupo
        {
            public int Id { get; set; }
            public int Grado { get; set; }
            public int Numero { get; set; }
            public int Anio { get; set; }
            public int OrientacionId { get; set; }
            public int TurnoId { get; set; }
        }

        public class CSGetHM
        {
            public int Id { get; set; }
            public int Hora { get; set; }
            public CSGetSalon Salon { get; set; }
            public string Materia { get; set; }
            public int GrupoId { get; set; }
            public string Dia { get; set; }
            public GrupoDto Grupo { get; set; }
        }

        public class GrupoDto
        {
            public int Id { get; set; }
            public int Grado { get; set; }
            public int Numero { get; set; }
            public int Anio { get; set; }
            public int OrientacionId { get; set; }
            public string Turno { get; set; }
        }

        public class CSPost
        {
            public DateTime Fecha { get; set; }
            public int SalonIdInicial { get; set; }
            public int SalonIdDestino { get; set; }
            public int HoraMateriaIdInicial { get; set; }
            public int HoraMateriaIdFinal { get; set; }
            public int? SalonIdFinal { get; set; }
        }

        [HttpGet]
        public IActionResult GetCreated()
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            IEnumerable<CambioDeSalon> csList = _repo.GetAll();
            List<CSGet> product = new List<CSGet>();
            foreach (CambioDeSalon cs in csList)
            {
                if(cs.CreatorId == userID)
                {
                    product.Add(DtoGet(cs));
                }

            }

            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetRelated()
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Alumno alumno = _alumnos.GetById(userID);

            IEnumerable<CambioDeSalon> csList = _repo.GetAll();
            List<CSGet> product = new List<CSGet>();
            foreach (CambioDeSalon cs in csList)
            {
                if (cs.HoraMateriaInicial.Grupo == alumno.Grupo)
                {
                    product.Add(DtoGet(cs));
                }
            }

            return Ok(product);
        }

        public class CSDelete
        {
            public int Id { get; set; }
        }

        [HttpPost]
        public IActionResult Cancel([FromBody] CSDelete dto)
        {
            CambioDeSalon cs = _repo.GetById(dto.Id);
            _repo.Delete(cs);
            return Ok(cs);
        }


        [HttpPost]
        public IActionResult Post([FromBody] CSPost dto)
        {
            DateTime fecha = dto.Fecha;
            Salon salonInicial = _salones.GetById(dto.SalonIdInicial);
            Salon salonDestino = _salones.GetById(dto.SalonIdDestino);
            Salon salonFinal = null;
            if (dto.SalonIdFinal != null)
            {
                int salonFinalId = (int) dto.SalonIdFinal;
                salonFinal = _salones.GetById(salonFinalId);
            }
            HoraMateria hmInicial = _horas.GetById(dto.HoraMateriaIdInicial);
            HoraMateria hmFinal = _horas.GetById(dto.HoraMateriaIdFinal);

            CambioDeSalon cs = new CambioDeSalon
            {
                HoraMateriaFinal = hmFinal,
                HoraMateriaInicial = hmInicial,
                SalonCambio = salonDestino,
                SalonFinal = salonFinal,
                SalonInicial = salonInicial
            };

            _repo.Add(cs);
            return Ok();
        }

    }

}