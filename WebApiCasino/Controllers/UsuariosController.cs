using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiCasino.DTOs.Autenticacion;
using WebApiCasino.Entidades;

namespace WebApiCasino.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]

        private readonly UserManager<IdentityUser> userManager;
        readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsuariosController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public async Task<ActionResult<DatosAutenticacion>> Registrar(RegistroDTO registroDTO)
        {
            var user = new IdentityUser { UserName = registroDTO.Email, Email = registroDTO.Email };
            var result = await userManager.CreateAsync(user, registroDTO.Password);

            var data = await userManager.FindByEmailAsync(registroDTO.Email);

            if (data != null)
            {
                if (registroDTO.IsAdmin == 1)
                {
                    await userManager.AddClaimAsync(data, new Claim("EsAdmin", "1"));
                }
                else
                {
                    await userManager.AddClaimAsync(data, new Claim("EsParticipante", "1"));
                }
            }

            if (result.Succeeded)
            {
                //Se retorna el Jwt (Json Web Token) especifica el formato del token que hay que devolverle a los clientes
                return await ConstruirToken(data);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<DatosAutenticacion>> LoginDTO(LoginDTO loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return BadRequest("Wrong Password");
            } else
            {
                return await ConstruirToken(user);
            }
        }

        private async Task<ActionResult<DatosAutenticacion>> ConstruirToken(IdentityUser usuario)
        {
            var claims = new List<Claim>
                {
                new Claim("UserId", usuario.Id),
                new Claim("UserEmail", usuario.Email),
                new Claim("UserName", usuario.UserName),
                };


            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new DatosAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }


    }
}
