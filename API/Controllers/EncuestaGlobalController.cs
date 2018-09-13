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

namespace API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("[controller]/[action]")]
    public class EncuestaGlobalController : Controller
    {
        private readonly IRepository<EncuestaGlobal> _repo;
        private readonly IUserRepository<Adscripto> _adscriptos;
        private readonly IUserRepository<Alumno> _alumnos;
        private readonly UserManager<AppUser> _userManager;

        public EncuestaGlobalController(IRepository<EncuestaGlobal> repo, IUserRepository<Adscripto> adscriptos, IUserRepository<Alumno> alumnos, UserManager<AppUser> userManager)
        {
            this._repo = repo;
            this._adscriptos = adscriptos;
            this._alumnos = alumnos;
            this._userManager = userManager;
        }

        [HttpGet("{id}", Name = "GetEncuesta")]
        public IActionResult Get(int id)
        {
            EncuestaGlobal encuesta = _repo.GetById(id);
            if(encuesta == null)
            {
                return NotFound();
            }
            return Ok(ByIdDtoGet(encuesta));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCreated()
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser appUser = _userManager.Users.SingleOrDefault(r => r.Id == userID);
            Adscripto adscripto = _adscriptos.GetById(appUser.Id);

            List<PostEncuestaGlobalDto> dto = new List<PostEncuestaGlobalDto>();
            foreach(EncuestaGlobal encuesta in _repo.GetAll())
            {
                if(encuesta.AdscriptoId == adscripto.Id)
                {
                    dto.Add(DtoGet(encuesta));
                }
            }
            return Ok(dto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser appUser = _userManager.Users.SingleOrDefault(r => r.Id == userID);
            Alumno alumno = _alumnos.GetById(appUser.Id);

            List<PostEncuestaGlobalDto> dto = new List<PostEncuestaGlobalDto>();
            foreach (EncuestaGlobal encuesta in _repo.GetAll())
            {
                var hasResponded = false;
                foreach(Pregunta p in encuesta.Preguntas)
                {
                    if(p is PreguntaUO)
                    {
                    }

                }

                if(!hasResponded)
                {
                    dto.Add(DtoGet(encuesta));
                }
            }
            return Ok(dto);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] PostEncuestaGlobalDto encuesta)
        {
            if (encuesta == null)
                return BadRequest();

            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser appUser = _userManager.Users.SingleOrDefault(r => r.Id == userID);
            Adscripto adscripto = _adscriptos.GetById(appUser.Id);

            var _encuesta = new EncuestaGlobal
            {
                Id = encuesta.Id,
                Titulo = encuesta.Titulo,
                Descripcion = encuesta.Descripcion,
                FechaCreacion = encuesta.FechaCreacion,
                Adscripto = adscripto,
                AdscriptoId = adscripto.Id,
                Preguntas = new List<Pregunta>()
            };

            foreach(PostPreguntaDto pregunta in encuesta.Preguntas)
            {
                switch(pregunta.Tipo)
                {
                }
                _encuesta.Preguntas.Add(new Pregunta
                {
                    Id = pregunta.Id,
                    Texto = pregunta.Texto
                });
            }

            EncuestaGlobal addEncuesta = _repo.Add(_encuesta);
            return CreatedAtRoute("GetEncuesta", new
            {
                id = addEncuesta.Id
            }, new
            {
                Encuesta = DtoGet(addEncuesta)
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Respond([FromBody] RespondDto dto)
        {
            if (dto == null)
                return BadRequest();

            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser appUser = _userManager.Users.SingleOrDefault(r => r.Id == userID);
            Alumno alumno = _alumnos.GetById(appUser.Id);

            EncuestaGlobal encuesta = _repo.GetById(dto.EncuestaId);
            if(encuesta == null)
            {
                return NotFound();
            }

            foreach(Pregunta pregunta in encuesta.Preguntas)
            {
                foreach(RespondPreguntaDto r in dto.List)
                {
                    if(r.PreguntaId == pregunta.Id)
                    {
                        //var index = encuesta.Preguntas.IndexOf(pregunta);
                        //var respuesta = new Respuesta()
                        //{
                        //    Alumno = alumno,
                        //    Valores = new List<Valor>()
                        //};

                        //foreach(string valor in r.Valores)
                        //{
                        //    respuesta.Valores.Add(new Valor() {
                        //        Contenido = valor
                        //    });
                        //}

                        //encuesta.Preguntas[index].Respuestas.Add(respuesta);
                    }
                }
            }

            _repo.Update(encuesta);
            return Ok();
        }

        public PostEncuestaGlobalDto DtoGet(EncuestaGlobal encuesta)
        {
            //PostEncuestaGlobalDto encuestaDto = new PostEncuestaGlobalDto
            //{
            //    Id = encuesta.Id,
            //    Titulo = encuesta.Titulo,
            //    Descripcion = encuesta.Descripcion,
            //    FechaCreacion = encuesta.FechaCreacion,
            //    Preguntas = new List<PostPreguntaDto>()
            //};

            //foreach (Pregunta pregunta in encuesta.Preguntas)
            //{
            //    encuestaDto.Preguntas.Add(new PostPreguntaDto
            //    {
            //        Id = pregunta.Id,
            //        Texto = pregunta.Texto,
            //        Tipo = pregunta.Tipo
            //    });
            //}

            return null;
        }

        public GetByIdEncuestaDto ByIdDtoGet(EncuestaGlobal encuesta)
        {
            GetByIdEncuestaDto encuestaDto = new GetByIdEncuestaDto
            {
                Id = encuesta.Id,
                Titulo = encuesta.Titulo,
                Descripcion = encuesta.Descripcion,
                FechaCreacion = encuesta.FechaCreacion,
                Preguntas = new List<GetByIdPreguntaDto>()
            };

            //foreach (Pregunta pregunta in encuesta.Preguntas)
            //{
            //    GetByIdPreguntaDto p = new GetByIdPreguntaDto
            //    {
            //        Id = pregunta.Id,
            //        Texto = pregunta.Texto,
            //        Tipo = pregunta.Tipo,
            //        Respuestas = new List<GetByIdRespuestaDto>()
            //    };
            //    foreach(Respuesta r in pregunta.Respuestas)
            //    {
            //        var _r = new GetByIdRespuestaDto
            //        {
            //            Id = r.Id,
            //            Valores = new List<string>()
            //        };
            //        foreach (Valor v in r.Valores)
            //        {
            //            _r.Valores.Add(v.Contenido);
            //        }
            //        p.Respuestas.Add(_r);
            //    }
            //}

            return encuestaDto;
        }

    }

    public class RespondDto
    {
        public int EncuestaId { get; set; }
        public List<RespondPreguntaDto> List { get; set; }
    }

    public class PostEncuestaGlobalDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public List<PostPreguntaDto> Preguntas { get; set; }
    }

    public class GetByIdEncuestaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public List<GetByIdPreguntaDto> Preguntas { get; set; }
    }

    public class GetByIdPreguntaDto
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Tipo { get; set; }
        public List<GetByIdRespuestaDto> Respuestas { get; set; }
    }

    public class GetByIdRespuestaDto
    {
        public int Id { get; set; }
        public List<string> Valores { get; set; }
    }

    public class PostPreguntaDto
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public string Texto { get; set; }
    }

    public class RespuestaDto
    {
        public int Id { get; set; }
        public List<string> Valores { get; set; }
    }

    public class RespondPreguntaDto
    {
        public int PreguntaId { get; set; }
        public List<string> Valores { get; set; }
    }
}