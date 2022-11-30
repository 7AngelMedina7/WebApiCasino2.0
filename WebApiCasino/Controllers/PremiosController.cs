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
        public PremiosController(ApplicationDbContext dbContext, IMapper mapper)
        {
            //Inyeccion de dependecias
            this.mapper = mapper;
            this.dbContext = dbContext;
        }
        public ApplicationDbContext dbContext { get; }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PremioDTO objPremio)
        {
            var veifircarPremio = await dbContext.Premios.AnyAsync(x => x.Recompensa == objPremio.Recompensa);
            if (veifircarPremio)
            {
                return BadRequest("Ya existe");
            }
            var premioAux = mapper.Map<Premio>(objPremio);
            dbContext.Add(premioAux);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
