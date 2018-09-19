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
            IEnumerable<EncuestaGlobal> list = _repo.GetAll();
            List<EGGetEncuesta> dtoList = new List<EGGetEncuesta>();

            foreach (EncuestaGlobal eg in list)
            {
                dtoList.Add(DtoGet(eg));
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

        [HttpPost]
        public IActionResult Respond([FromBody] EGRespond encuesta)
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Alumno alumno = _alumnos.GetById(userID);

            var toRespond = _repo.GetById(encuesta.Id);

            foreach (EGRespondPregunta pregunta in encuesta.Preguntas)
            {
                foreach (Pregunta pEntity in toRespond.Preguntas)
                {
                    if(pEntity.Id == pregunta.Id)
                    {
                        if(pEntity is PreguntaEL)
                        {
                            var xPregunta = (EGRespondPreguntaLibre)pregunta;
                            var el = (PreguntaLibre)pEntity;
                            el.Respuestas.Add(new RespuestaLibre
                            {
                                Texto = xPregunta.Respuesta.Texto
                            });
                        } else if(pEntity is PreguntaMO)
                        {
                            var xPregunta = (EGRespondPreguntaVariada)pregunta;
                            var el = (PreguntaMO)pEntity;
                            foreach (EGRespondRespuestaVariada pv in xPregunta.Respuestas)
                            {
                                foreach(int opcion in pv.Opciones)
                                {
                                    var respuesta = new RespuestaMO
                                    {
                                        Alumno = alumno,
                                        Pregunta = pEntity,
                                        Respuestas = new List<OpcionRespuesta>()
                                    };
                                    
                                    var toRespondRespuesta = new OpcionRespuesta
                                    {
                                        Respuesta = respuesta,
                                        OpcionId = opcion
                                    };

                                    respuesta.Respuestas.Add(toRespondRespuesta);
                                    el.Respuestas.Add(respuesta);
                                }
                            }
                        } else if(pEntity is PreguntaUO)
                        {
                            var xPregunta = (EGRespondPreguntaVariada)pregunta;
                            var el = (PreguntaUO)pEntity;
                            foreach (EGRespondRespuestaVariada pv in xPregunta.Respuestas)
                            {
                                foreach (int opcion in pv.Opciones)
                                {
                                    var toRespondRespuesta = new RespuestaUO
                                    {
                                        Alumno = alumno,
                                        Pregunta = pEntity,
                                        RespuestaOpcion = new PreguntaOpcion
                                        {
                                            Id = pv.Opciones.First()
                                        }
                                    };

                                    el.Respuestas.Add(toRespondRespuesta);
                                }
                            }
                        }
                    }
                }

            }

            _repo.Update(toRespond);
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
                        Opciones = new List<EGGetOpcion>(),
                        Respuestas = new List<EGGetRespuesta>(),
                        Texto = pregunta.Texto,
                        Tipo = -1
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
                        proxyProductPregunta.Opciones.Add(new EGGetOpcion()
                        {
                            Texto = opcion.Valor
                        });
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

    public class EGRespondPregunta
    {
        public int Id { get; set; }
    }

    public class EGRespondPreguntaVariada : EGRespondPregunta
    {
        public List<EGRespondRespuestaVariada> Respuestas { get; set; }
    }

    public class EGRespondPreguntaLibre : EGRespondPregunta
    {
        public EGRespondRespuestaLibre Respuesta { get; set; }
    }

    public class EGRespondRespuesta
    {
        
    }

    public class EGRespondRespuestaLibre : EGRespondRespuesta
    {
        public string Texto { get; set; }
    }

    public class EGRespondRespuestaVariada : EGRespondRespuesta
    {
        public List<int> Opciones { get; set; }
    }

    public class EGRespond
    {
        public int Id { get; internal set; }
        internal IEnumerable<EGGetRespuesta> Respuestas { get; set; }
        internal IEnumerable<EGRespondPregunta> Preguntas { get; set; }
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
        public string Texto { get; set; }
    }

    public class EGGetRespuesta
    {
        public string Text { get; set; }
        public int OpcionId { get; set; }
    }

    public class EGGetRespuestaLibre : EGGetRespuesta
    {
        public string Texto { get; set; }
    }

    public class EGGetRespuestaVariada : EGGetRespuesta
    {

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