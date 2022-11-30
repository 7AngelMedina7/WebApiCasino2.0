using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApiCasino.DTOs;
using WebApiCasino.Entidades;

namespace WebApiCasino.Controllers
{
    [ApiController]
    [Route("/rifas")]
    public class RifasController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<RifasController> logger;
        
        public RifasController(ApplicationDbContext dbContext, IMapper mapper,ILogger<RifasController>logger)
        {
            //Inyeccion de dependecias

            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] RifaDTO objRifa)
        {
            var veifircarNombreRifa = await dbContext.Rifas.AnyAsync(x => x.Nombre == objRifa.Nombre);
            if (veifircarNombreRifa)
            {
                return BadRequest("Ya existe");
            }
            var rifaAux = mapper.Map<Rifa>(objRifa);
            rifaAux.CreationDate = DateTime.Now;
            dbContext.Add(rifaAux);
            await dbContext.SaveChangesAsync();
            return Ok($"El Id de la Rifa es: {rifaAux.Id}");
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Rifa>>> Get()
        {
            logger.LogInformation("Obteneniendo las Rifas");
            var rifas = dbContext.Rifas.ToList();
            return rifas;
        }

        [HttpGet("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<ActionResult<Rifa>> Get(int id)
        {
            var aux = await dbContext.Rifas.FirstOrDefaultAsync(db => db.Id == id);
            if (aux == null)
            {
                logger.LogWarning($"El autor de Id{id}no ha sido encontrado");
                return NotFound();
            }
            return aux;
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<GetRifaDTO>> Delete(int id)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                return NotFound("El recurso no fue encontrado");
            }
            dbContext.Rifas.Remove(new Rifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<GetRifaDTO>> Patch(int id, [FromBody] RifaDTO objRifa)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                return NotFound("El recurso no fue encontrado");
            }
            dbContext.Rifas.Update(new Rifa()
            {
                Id = id,
                Nombre = objRifa.Nombre
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<IActionResult> Put(int id, [FromBody] RifaDTO objRifa)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                return NotFound("El recurso no fue encontrado");
            }
            dbContext.Rifas.Update(new Rifa()
            {
                Id = id,
                Nombre = objRifa.Nombre
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        //Hacer cambio a get con las variables con int y string
        [HttpPost("search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Rifa>> Search([FromBody] BuscarRifaDTO buscarRifaDTO)
        {
            var aux = dbContext.Rifas.FirstOrDefault(db => db.Nombre == buscarRifaDTO.Nombre);
            if (buscarRifaDTO.Id != 0)
            {
                aux = await dbContext.Rifas.FirstOrDefaultAsync(db => db.Id == buscarRifaDTO.Id);
            } else
            {
                aux = dbContext.Rifas.FirstOrDefault(db => db.Nombre == buscarRifaDTO.Nombre);
            }
            return aux;
        }
        [AllowAnonymous]
        [HttpPatch("JsonPatch/{id}")]
        public async Task<IActionResult>Patch (int id, JsonPatchDocument<Rifa> rifasAux)
        {//ver como cambiar la forma en  la que pide en swagger Quiza con mapper
            var rifas = await dbContext.Rifas.FirstOrDefaultAsync(a => a.Id == id);
            //  [{"op":"replace", "path": "Nombre", "value": "test"}]
            logger.LogInformation("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            logger.LogWarning(rifasAux.ToString());
            if (rifas==null)
            {
                return NotFound();
            }
            
            rifasAux.ApplyTo(rifas);
            await dbContext.SaveChangesAsync();
            return Ok(rifas);
        }

    }
}
