using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<List<GetPremioDTO>>> Get()
        {
            var verificarPremios = await dbContext.Premios.ToListAsync();
            return mapper.Map<List<GetPremioDTO>>(verificarPremios);
        }

        [HttpPost] 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<AddRifaDTO>> Patch( [FromBody] PremioDTO premioDTO)
        {
            var existe = await dbContext.Premios.AnyAsync(a => a.Lugar == premioDTO.Lugar && a.RifaRefId == premioDTO.RifaId);
            if (existe)
            {
                return NotFound($"El premio con el lugar #{premioDTO.Lugar} ya existe.");
            }
            Premio premio = mapper.Map<Premio>(premioDTO);
            dbContext.Add(premio);
            await dbContext.SaveChangesAsync();

            var premioS = await dbContext.Premios.FirstOrDefaultAsync(a => a.Lugar == premioDTO.Lugar && a.RifaRefId == premioDTO.RifaId);

            return new AddRifaDTO() { message = "Premio registrado correctamente.", data = premioS };
        }

    }
}
