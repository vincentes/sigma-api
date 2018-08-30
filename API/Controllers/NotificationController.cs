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
        private readonly IRepository<EventoGrupo> _eventos;
        private readonly UserManager<AppUser> _userManager;

        public NotificationController(IRepository<Token> repo, IRepository<EventoGrupo> eventos, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _repo = repo;
            _eventos = eventos;
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
        public IActionResult EventNotify()
        {
            IEnumerable<EventoGrupo> eventos = _eventos.GetAll();
            foreach(EventoGrupo evento in eventos)
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