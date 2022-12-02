using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCasino.Entidades;

namespace WebApiCasino.Controllers
{
    [ApiController]
    [Route("cartas")]
    public class CartasController: ControllerBase
    {
        public CartasController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ApplicationDbContext dbContext { get; }

        //Obtener Todas las cartas.
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<List<Carta>>> Get()
        {
            var cartas = dbContext.Cartas.ToList();
            return cartas;
        }
    }
}
