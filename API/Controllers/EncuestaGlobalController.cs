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
        private readonly IRepository<Pregunta> _preguntas;
        private readonly IRepository<PreguntaOpcion> _opciones;
        private readonly IUserRepository<Adscripto> _adscriptos;
        private readonly IUserRepository<Alumno> _alumnos;
        private readonly UserManager<AppUser> _userManager;

        public EncuestaGlobalController(IRepository<EncuestaGlobal> repo, IRepository<PreguntaOpcion> opciones, IRepository<Pregunta> preguntas, IUserRepository<Adscripto> adscriptos, IUserRepository<Alumno> alumnos, UserManager<AppUser> userManager)
        {
            this._repo = repo;
            this._adscriptos = adscriptos;
            this._alumnos = alumnos;
            this._userManager = userManager;
            this._preguntas = preguntas;
            this._opciones = opciones;
        }

        [HttpGet("{id}", Name = "GetEncuestaGlobal")]
        public IActionResult Get(int id)
        {
            EncuestaGlobal byId = _repo.GetById(id);
            if (byId == null)
            {
                return NotFound();
            }

            return Ok(DtoGet(byId));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Alumno alumno = _alumnos.GetById(userID);

            IEnumerable<EncuestaGlobal> list = _repo.GetAll();
            List<EGGetEncuesta> dtoList = new List<EGGetEncuesta>();

            foreach (EncuestaGlobal eg in list)
            {
                List<bool> preguntaRespuestas = new List<bool>();
                foreach (Pregunta pregunta in eg.Preguntas)
                {
                    var preguntaRespondida = false;
                    if(pregunta is PreguntaMO)
                    {
                        var xPregunta = (PreguntaMO)pregunta;
                        foreach(PreguntaOpcion opcion in xPregunta.Opciones)
                        {
                            foreach(Respuesta respuesta in opcion.RespuestasAsociadas)
                            {
                                if(respuesta.AlumnoId == alumno.Id)
                                {
                                    preguntaRespondida = true;
                                }
                            }
                        }
                    } else if(pregunta is PreguntaLibre)
                    {
                        var xPregunta = (PreguntaLibre)pregunta;
                        foreach(RespuestaLibre respuesta in xPregunta.Respuestas)
                        {
                            if (respuesta.AlumnoId == alumno.Id)
                            {
                                preguntaRespondida = true;
                            }
                        }
                    } else
                    {
                        var xPregunta = (PreguntaVariada)pregunta;
                        foreach (RespuestaLimitada respuesta in xPregunta.Respuestas)
                        {
                            if (respuesta.AlumnoId == alumno.Id)
                            {
                                preguntaRespondida = true;
                            }
                        }
                    }

                    if (preguntaRespondida)
                    {
                        preguntaRespuestas.Add(true);
                        eg.Preguntas.Remove(pregunta);
                        break;
                    }
                    else
                    {
                        preguntaRespuestas.Add(false);
                    }
                }

                if(preguntaRespuestas.Contains(false))
                {
                    dtoList.Add(DtoGet(eg));
                }
            }

            return Ok(dtoList);
        }

        [HttpGet]
        public IActionResult GetCreated()
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Adscripto adscripto = _adscriptos.GetById(userID);

            IEnumerable<EncuestaGlobal> list = _repo.GetAll();
            List<EGGetEncuesta> dtoList = new List<EGGetEncuesta>();

            foreach (EncuestaGlobal eg in list)
            {
                if (eg.AdscriptoId == adscripto.Id)
                {
                    dtoList.Add(DtoGet(eg));
                }
            }

            return Ok(dtoList);
        }

        public class RespondMOPreguntaDto {
            public int Id { get; set; }
            public IEnumerable<int> Opciones { get; set; }
        }

        public class RespondUOPreguntaDto
        {
            public int Id { get; set; }
            public int Opcion { get; set; }
        }

        public class RespondELPreguntaDto
        {
            public int Id { get; set; }
            public string Texto { get; set; }
        }

        [HttpPost]
        public IActionResult RespondMOPregunta([FromBody] RespondMOPreguntaDto dto)
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Alumno alumno = _alumnos.GetById(userID);

            PreguntaMO pregunta = (PreguntaMO) _preguntas.GetById(dto.Id);
            foreach(int opcion in dto.Opciones)
            {
                var o = _opciones.GetById(opcion);
                o.RespuestasAsociadas.Add(new RespuestaLimitada
                {
                    Alumno = alumno,
                    Pregunta = pregunta
                });
                _opciones.Update(o);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult RespondUOPregunta([FromBody] RespondUOPreguntaDto dto)
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Alumno alumno = _alumnos.GetById(userID);

            PreguntaVariada pregunta = (PreguntaVariada)_preguntas.GetById(dto.Id);
            RespuestaUO respuesta = new RespuestaUO
            {
                Alumno = alumno,
                Pregunta = pregunta
            };
        
            respuesta.RespuestaOpcion = _opciones.GetById(dto.Opcion);
            foreach(Respuesta o in respuesta.RespuestaOpcion.RespuestasAsociadas)
            {
                if(o.AlumnoId == alumno.Id)
                {
                    return BadRequest();
                }
            }

            pregunta.Respuestas.Add(respuesta);
            _preguntas.Update(pregunta);
            return Ok();
        }

        [HttpPost]
        public IActionResult RespondELPregunta([FromBody] RespondELPreguntaDto dto)
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Alumno alumno = _alumnos.GetById(userID);

            PreguntaLibre pregunta = (PreguntaLibre)_preguntas.GetById(dto.Id);
            RespuestaLibre respuesta = new RespuestaLibre
            {
                Alumno = alumno,
                Pregunta = pregunta
            };

            respuesta.Texto = dto.Texto;
            pregunta.Respuestas.Add(respuesta);
            _preguntas.Update(pregunta);
            return Ok();
        }


        public EGGetEncuesta DtoGet(EncuestaGlobal encuesta)
        {
            var product = new EGGetEncuesta()
            {
                Id = encuesta.Id,
                Titulo = encuesta.Titulo,
                Descripcion = encuesta.Descripcion,
                Preguntas = new List<EGGetPregunta>()
            };

            foreach (Pregunta pregunta in encuesta.Preguntas)
            {
                EGGetPregunta productPregunta = new EGGetPregunta();
                if (pregunta is PreguntaLibre)
                {
                    var proxyProductPregunta = new EGGetPreguntaLibre()
                    {
                        Id = pregunta.Id,
                        Respuestas = new List<EGGetRespuesta>(),
                        Texto = pregunta.Texto
                    };

                    var _pregunta = (PreguntaLibre)pregunta;
                    if (_pregunta.Respuestas != null)
                    {
                        foreach (RespuestaLibre rl in _pregunta.Respuestas)
                        {
                            proxyProductPregunta.Respuestas.Add(new EGGetRespuestaLibre
                            {
                                Texto = rl.Texto
                            });
                        }
                    }

                    product.Preguntas.Add(proxyProductPregunta);
                }
                else if (pregunta is PreguntaVariada)
                {
                    var proxyProductPregunta = new EGGetPreguntaVariada()
                    {
                        Id = pregunta.Id,
                        Opciones = new List<EGGetOpcion>(),
                        Respuestas = new List<EGGetRespuesta>(),
                        Texto = pregunta.Texto,
                        Tipo = 2
                    };

                    if (pregunta is PreguntaMO)
                    {
                        proxyProductPregunta.Tipo = 3;
                    } else if (pregunta is PreguntaUO)
                    {
                        proxyProductPregunta.Tipo = 1;
                    } else if (pregunta is PreguntaEL)
                    {
                        proxyProductPregunta.Tipo = 0;
                    }

                    var _pregunta = (PreguntaVariada)pregunta;
                    foreach (PreguntaOpcion opcion in _pregunta.Opciones)
                    {
                        var cast = opcion;
                        if(opcion.RespuestasAsociadas != null)
                        {
                            if(_pregunta is PreguntaMO)
                            {
                                proxyProductPregunta.Opciones.Add(new EGGetOpcion()
                                {
                                    Id = opcion.Id,
                                    Texto = opcion.Valor,
                                    Respuestas = opcion.RespuestasAsociadas.Count
                                });
                            } else
                            {
                                proxyProductPregunta.Opciones.Add(new EGGetOpcion()
                                {
                                    Id = opcion.Id,
                                    Texto = opcion.Valor,
                                    Respuestas = _pregunta.Respuestas.Count
                                });
                            }
                        } else
                        {
                            proxyProductPregunta.Opciones.Add(new EGGetOpcion()
                            {
                                Id = opcion.Id,
                                Texto = opcion.Valor,
                                Respuestas = 0
                            });
                        }
                    }

                    if (_pregunta.Respuestas != null)
                    {
                        foreach (RespuestaLimitada rl in _pregunta.Respuestas)
                        {
                            if(rl is RespuestaUO)
                            {
                                proxyProductPregunta.Respuestas.Add(new EGGetRespuestaVariada
                                {
                                    OpcionId = ((RespuestaUO)rl).RespuestaOpcion.Id,
                                    Text = ((RespuestaUO)rl).RespuestaOpcion.Valor
                                });
                            } else if(rl is RespuestaMO)
                            {
                                var xRl = (RespuestaMO)rl;
                                foreach(PreguntaOpcion opcion in xRl.RespuestaOpciones)
                                {
                                    proxyProductPregunta.Respuestas.Add(new EGGetRespuestaVariada
                                    {
                                        OpcionId = opcion.Id,
                                        Text = opcion.Valor
                                    });
                                }
                            }
                        }
                    }
                    product.Preguntas.Add(proxyProductPregunta);
                }
            }
            return product;
        }

        [HttpPost]
        public IActionResult Post([FromBody] EGPostEncuesta dto)
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Adscripto adscripto = _adscriptos.GetById(userID);

            EncuestaGlobal toPost = new EncuestaGlobal
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                FechaCreacion = DateTime.Now,
                Adscripto = adscripto,
                Preguntas = new List<Pregunta>()
            };

            foreach (EGPostPregunta dtoPregunta in dto.Preguntas)
            {
                switch (dtoPregunta.Tipo)
                {
                    case 0:
                        toPost.Preguntas.Add(new PreguntaEL
                        {
                            Texto = dtoPregunta.Texto,
                            Respuestas = new List<RespuestaLibre>()
                        });
                        break;
                    case 1:
                        var preguntaToPost = new PreguntaUO
                        {
                            Texto = dtoPregunta.Texto,
                            Opciones = new List<PreguntaOpcion>()
                        };

                        foreach (string dtoRespuesta in dtoPregunta.Respuestas)
                        {
                            preguntaToPost.Opciones.Add(new PreguntaOpcion
                            {
                                Pregunta = preguntaToPost,
                                Valor = dtoRespuesta
                            });
                        }
                        toPost.Preguntas.Add(preguntaToPost);
                        break;
                    case 2:
                    case 3:
                        var moPreguntaToPost = new PreguntaMO
                        {
                            Texto = dtoPregunta.Texto,
                            Opciones = new List<PreguntaOpcion>()
                        };

                        foreach (string dtoRespuesta in dtoPregunta.Respuestas)
                        {
                            moPreguntaToPost.Opciones.Add(new PreguntaOpcion
                            {
                                Pregunta = moPreguntaToPost,
                                Valor = dtoRespuesta
                            });
                        }
                        toPost.Preguntas.Add(moPreguntaToPost);
                        break;
                }
            }

            _repo.Add(toPost);
            return Ok(DtoGet(toPost));
        }
    }

    public class EGGetEncuesta
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public List<EGGetPregunta> Preguntas { get; set; }
    }

    public class EGGetPregunta
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Tipo { get; set; }
    }

    public class EGGetPreguntaLibre : EGGetPregunta
    {
        public List<EGGetRespuesta> Respuestas { get; set; }
    }
    public class EGGetPreguntaVariada : EGGetPregunta
    {
        public List<EGGetOpcion> Opciones { get; set; }
        public List<EGGetRespuesta> Respuestas { get; set; }
    }
    public class EGGetOpcion
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Respuestas { get; set; }
    }

    public class EGGetRespuesta
    {
        public string Text { get; set; }
    }

    public class EGGetRespuestaLibre : EGGetRespuesta
    {
        public string Texto { get; set; }
    }

    public class EGGetRespuestaVariada : EGGetRespuesta
    {

        public int OpcionId { get; set; }
    }

    public class EGPostEncuesta
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public List<EGPostPregunta> Preguntas { get; set; }
    }

    public class EGPostPregunta
    {
        public string Texto { get; set; }
        public int Tipo { get; set; }
        public List<string> Respuestas { get; set; }
    }


}