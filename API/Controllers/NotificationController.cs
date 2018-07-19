using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly IRepository<Token> _repo;
        private readonly UserManager<AppUser> _userManager;

        public NotificationController(IRepository<Token> repo, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveToken([FromBody] TokenDto token) {
            var exists = _repo.GetAll().FirstOrDefault(e => e.Content == token.Token) != null;
            if(exists)
            {
                return StatusCode((int) HttpStatusCode.Conflict);
            }

            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            AppUser user = _userManager.Users.SingleOrDefault(r => r.Id == userID);
            _repo.Add(new Token()
            {
                Content = token.Token
            });
            return Ok();
        }

        public class TokenDto
        {
            public string Token { get; set; }
        }
    }
}