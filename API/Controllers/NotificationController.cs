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

                DateTime now = DateTime.Now;
                DateTime deadline = evento.Date;
                int daysDifference = (deadline - now).Days;
                string dayName = deadline.ToString("dddd", new CultureInfo("es-ES"));

                if (daysDifference <= 5 && !evento.Notified)
                {
                    string title;
                    string body;
                    DateTime sent = DateTime.Now;
                    if (evento.Evento is Escrito)
                    {
                        title = "Escrito próximo";
                        body = "¡Tenés un escrito este " + dayName + "!";
                    }
                    else if (evento.Evento is Parcial)
                    {
                        title = "Parcial próximo";
                        body = "¡Tenés un parcial este " + dayName + "!";
                    }
                    else if (evento.Evento is Tarea)
                    {
                        title = "Entrega próxima";
                        body = "¡Tenés que entregar un deber este " + dayName + "!";
                    }
                    else
                    {
                        continue;
                    }
                    

                    foreach(Alumno alumno in evento.Grupo.Alumnos)
                    {
                        foreach(Token token in alumno.Token)
                        {
                            Firebase.SendNotification(token.Content, title, body);
                        }
                    }
                }
            }
            return Ok();
        }
        

        public class TokenDto
        {
            public string Token { get; set; }
        }
    }
}