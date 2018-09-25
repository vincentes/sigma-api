using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class NotificationController : Controller
    {
        private readonly IRepository<Token> _repo;
        private readonly IRepository<TareaGrupo> _tg;
        private readonly IRepository<ParcialGrupo> _pg;
        private readonly IRepository<EscritoGrupo> _eg;
        private readonly UserManager<AppUser> _userManager;

        public NotificationController(IRepository<Token> repo, IRepository<TareaGrupo> tg, IRepository<ParcialGrupo> pg, IRepository<EscritoGrupo> eg, IRepository<EscritoGrupo> eventos, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _repo = repo;
            _tg = tg;
            _pg = pg;
            _eg = eg;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult SaveToken([FromBody] TokenDto token) {
            var exists = _repo.GetAll().FirstOrDefault(e => e.Content == token.Token) != null;
            if (exists)
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }

            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser user = _userManager.Users.SingleOrDefault(r => r.Id == userID);
            _repo.Add(new Token()
            {
                Content = token.Token,
                User = user
            });
            return Ok();
        }    

        [HttpPost]
        public IActionResult DeleteToken()
        {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser user = _userManager.Users.SingleOrDefault(r => r.Id == userID);

            RToken rToken = (RToken)this._repo;
            rToken.DeleteByUserId(user.Id);
            return Ok();
        }
    
        [HttpPost]
        public IActionResult EventNotify()
        {
            IEnumerable<ParcialGrupo> parciales = _pg.GetAll();
            IEnumerable<EscritoGrupo> escritos = _eg.GetAll();
            IEnumerable<TareaGrupo> tareas = _tg.GetAll();
            foreach (ParcialGrupo evento in parciales)
            {
                Firebase.RemindEvents(evento);
            }

            foreach (EscritoGrupo evento in escritos)
            {
                Firebase.RemindEvents(evento);
            }

            foreach (TareaGrupo evento in tareas)
            {
                Firebase.RemindEvents(evento);
            }

            return Ok();
        }
        

        public class TokenDto
        {
            public string Token { get; set; }
        }
    }
}