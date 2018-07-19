using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("[controller]")]
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly IRepository<Materia> _repo;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificationController(IRepository<Materia> repo, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _repo = repo;
            this._userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveToken(string token) {
            var ident = User.Identity as ClaimsIdentity;
            var userID = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            IdentityUserExt user = (IdentityUserExt) _userManager.Users.SingleOrDefault(r => r.Id == userID);
            SigmaUser sigmaUser = user.SigmaUser;
            sigmaUser.Tokens.Add(new Token { Content = token });
            return Ok();
        }
    }
}