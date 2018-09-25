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
    public class AlertaController : Controller
    {
        private readonly IRepository<Alerta> _repo;

        public AlertaController(IRepository<Alerta> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Alerta> all = this._repo.GetAll();
            foreach(Alerta alerta in all)
            {
                return Ok(DtoGet(all.First()));
            }
            return Ok();
        }

        public AlertaDto DtoGet(Alerta escrito)
        {
            AlertaDto alerta = new AlertaDto()
            {
                Estado = escrito.Estado
            };

            return alerta;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AlertaDto escrito)
        {
            var alerta = new Alerta
            {
                Estado = escrito.Estado
            };
            // fake alerta
            this._repo.Delete(alerta);
            _repo.Add(alerta);
            return Ok();
        }

        public class PostAlertaDto
        {
            public int Id { get; set; }
            public int MateriaId { get; set; }
            public string Temas { get; set; }
            public DateTime Date { get; set; }
            public List<GrupoDto> GruposAsignados { get; set; }
        }

        public class AlertaDto
        {
            public string Estado { get; set; }
        }


    }

}
