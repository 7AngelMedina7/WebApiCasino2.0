using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCasino.DTOs;
using WebApiCasino.Entidades;

namespace WebApiCasino.Controllers
{
        [ApiController]
        [Route("/premios")]
    public class PremiosController: ControllerBase
    {
        private readonly IMapper mapper;
        public ApplicationDbContext dbContext { get; }
        public PremiosController(ApplicationDbContext dbContext, IMapper mapper)
        {
            //Inyeccion de dependecias
            this.mapper = mapper;
            this.dbContext = dbContext;
        }
        [HttpGet("VerPremios")]
        public async Task<ActionResult<PremioDTO>> Get()
        {
            var verificarPremios =  dbContext.Premios.Where(db => db.Recompensa != string.Empty);
            if (verificarPremios == null)
            {
                return NotFound();
            }
            return Ok(verificarPremios);
        }

        [HttpPatch("Añadir_premio{id:int}")]
        public async Task<ActionResult<AñadirPremioPatchDTO>> Patch(int id, [FromBody] PremioDTO objRifa)
        {
            var existe = await dbContext.Premios.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                return NotFound("El recurso no fue encontrado");
            }
            dbContext.Premios.Update(new Premio()
            {
                Lugar = objRifa.Lugar,
                Recompensa = objRifa.Recompensa
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] PremioDTO objPremio)
        //{
        //    var veifircarPremio = await dbContext.Premios.AnyAsync(x => x.Recompensa == objPremio.Recompensa);
        //    if (veifircarPremio)
        //    {
        //        return BadRequest("Ya existe");
        //    }
        //    var premioAux = mapper.Map<Premio>(objPremio);
        //    dbContext.Add(premioAux);
        //    await dbContext.SaveChangesAsync();
        //    return Ok();
        //}
