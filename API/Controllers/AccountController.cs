using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository<Docente> _docenteManager;
        private readonly IConfiguration _configuration;
        private readonly string[] _roles;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IUserRepository<Docente> docenteManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _roles = new string[3];
            _roles[0] = "Admin";
            _roles[1] = "Alumno";
            _roles[2] = "Docente";
            _signInManager = signInManager;
            _docenteManager = docenteManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<object> Login([FromBody]LoginDto model)
        {
            var login = await _signInManager.PasswordSignInAsync(model.CI, model.Password, false, false);
            if (login.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.CI);
                var roles = await _userManager.GetRolesAsync(appUser);

                if(appUser is Docente)
                {
                    var docente = _docenteManager.GetById(appUser.Id);
                    DocenteInfoDto result = new DocenteInfoDto();
                    result.Grupos = new List<GrupoDto>();
                    foreach (GrupoDocente gd in docente.GrupoDocentes)
                    {
                        result.Id = docente.Id;
                        result.Username = docente.UserName;
                        result.MateriaId = docente.MateriaId;
                        result.Grupos.Add(new GrupoDto
                        {
                            Id = gd.Grupo.Id,
                            Anio = gd.Grupo.Anio,
                            Grado = gd.Grupo.Grado,
                            Numero = gd.Grupo.Numero,
                            OrientacionId = gd.Grupo.OrientacionId,
                            TurnoId = gd.Grupo.TurnoId
                        });
                    }

                    return Ok(new {
                        Token = await GenerateJwtToken(model.CI, appUser),
                        Roles = roles,
                        Info = result
                    });
                } else if(appUser is Alumno)
                {
                    return Ok(new
                    {
                        Token = await GenerateJwtToken(model.CI, appUser),
                        Roles = roles
                    });
                }

            }
            return Unauthorized();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAlumno([FromBody] RegisterAlumnoDto model)
        {
            var user = new Alumno();
            user.UserName = model.CI;
            user.GrupoId = model.GrupoId;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(user.UserName);
                foreach (var item in _roles)
                {
                    if (!await _roleManager.RoleExistsAsync(item))
                    {
                        var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(item));
                        if (!createRoleResult.Succeeded)
                        {
                            throw new ApplicationException("Creating role " + item + "failed with error(s): " + createRoleResult.Errors);
                        }
                    }
                }

                if (!await _userManager.IsInRoleAsync(user: user, role: "Alumno"))
                {
                    var assignRoleResult = await _userManager.AddToRoleAsync(user, "Alumno");
                    if (!assignRoleResult.Succeeded)
                    {
                        throw new ApplicationException("Creating role 'Alumno' failed with error(s): " + assignRoleResult.Errors);
                    }
                }

                await _signInManager.SignInAsync(user, false);
                return Ok(new
                {
                    Token = await GenerateJwtToken(model.CI, user)
                });
            }
            return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDocente([FromBody] RegisterDocenteDto model)
        {
            var user = new Docente();
            user.UserName = model.CI;
            user.MateriaId = model.MateriaId;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(user.UserName);
                foreach (var item in _roles)
                {
                    if (!await _roleManager.RoleExistsAsync(item))
                    {
                        var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(item));
                        if (!createRoleResult.Succeeded)
                        {
                            throw new ApplicationException("Creating role " + item + "failed with error(s): " + createRoleResult.Errors);
                        }
                    }
                }

                if (!await _userManager.IsInRoleAsync(user: user, role: "Docente"))
                {
                    var assignRoleResult = await _userManager.AddToRoleAsync(user, "Docente");
                    if (!assignRoleResult.Succeeded)
                    {
                        throw new ApplicationException("Creating role 'Docente' failed with error(s): " + assignRoleResult.Errors);
                    }
                }

                if (await _userManager.IsInRoleAsync(user, "Docente"))
                {
                    var docente = (Docente)currentUser;
                    _docenteManager.Add(docente);
                }

                await _signInManager.SignInAsync(user, false);
                return Ok(new
                {
                    Token = await GenerateJwtToken(model.CI, user)
                });
            }
            return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new AppUser();

            user.UserName = model.CI;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(new
                {
                    Token = await GenerateJwtToken(model.CI, user)
                });
            }
            return StatusCode((int)HttpStatusCode.Conflict);
        }

        [Authorize]
        [HttpGet]
        public async Task<object> Protected()
        {
            return "!!! THOTS BEGONE !!!";
        }

        private async Task<object> GenerateJwtToken(string ci, AppUser user)
        {
            IdentityOptions options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userClaims);
            foreach(var role in userRoles) { 
                claims.Add(new Claim(ClaimTypes.Role, role));
                var r = await _roleManager.FindByNameAsync(role);
                if(r != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(r);
                    foreach(Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    internal class DocenteInfoDto
    {
        public List<GrupoDto> Grupos { get; internal set; }
        public string Id { get; internal set; }
        public int MateriaId { get; internal set; }
        public string Username { get; internal set; }
    }

    public class LoginDto
    {
        [Required]
        public string CI { get; set; }
        [Required]
        public string Password { get; set; }
    }


    public class RegisterDto
    {
        [Required]
        public string CI { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }

    public class RegisterAlumnoDto
    {
        [Required]
        public string CI { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public int GrupoId { get; set; }
    }

    public class RegisterDocenteDto
    {
        [Required]
        public string CI { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public int MateriaId { get; set; }
    }
}
