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
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpPost]
        //Verificar si tiene el claim "EsAdmin"
        //Postea una rifa nueva
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<AddRifaDTO>> Post([FromBody] RifaDTO obj)
        {
            //Verifica si el nombre de la risa ya existe en la base de batos
            var veifircarNombreRifa = await dbContext.Rifas.AnyAsync(x => x.Nombre == obj.Nombre);
            if (veifircarNombreRifa)
            {
                return BadRequest($"Ya existe el nombre {obj.Nombre}");
            }
            var rifaAux = mapper.Map<Rifa>(obj);
            dbContext.Add(rifaAux);
            await dbContext.SaveChangesAsync();

            var aux = await dbContext.Rifas.FirstOrDefaultAsync(x => x.Nombre == obj.Nombre);

            AddRifaDTO addRifa = new AddRifaDTO() { message = $"El Id de la Rifa es: {rifaAux.Id}", data = mapper.Map<GetRifaDTO>(aux) };
            return Ok(addRifa);
        }

        //Obtiene todas las rifas registradas
        [HttpGet]
        //Verifica que el usuario este registrado antes de usarlo
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<GetRifaDTO>>> Get()
        {
            logger.LogInformation("Obteneniendo las Rifas");
            var rifas = await dbContext.Rifas.ToListAsync();
            return mapper.Map<List<GetRifaDTO>>(rifas);
        }

        //Obtiene las rifas dandole el "id"
        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RifaDTO>> GetById(int id)
        {
            var aux = await dbContext.Rifas.FirstOrDefaultAsync(db => db.Id == id);
            if (aux == null)
            {
                logger.LogWarning($"La Rifa con el Id: {id} no ha sido encontrado");
                return NotFound();
            }
            return mapper.Map<RifaDTO>(aux);
        }
        //Obtiene los premios de la rifa proporcionada por un "id"
        [HttpGet("{id:int}/premios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PremioDTO>> GetPremios()
        {
            var verificarPremios = dbContext.Premios.Where(db => db.Recompensa != string.Empty);
            if (verificarPremios == null)
            {
                return NotFound();
            }
            PremioDTO premioAux = mapper.Map<PremioDTO>(verificarPremios);
            return Ok(premioAux);
        }

        //Obtiene los boletos disponibles de una rifa proporcionada dado su "Id"
        [HttpGet("{id:int}/boletos-disponibles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Carta>>> GetBoletosDisponibles(int id)
        {
            List<Carta> boletosDisponibles = dbContext.Cartas.Where(x => !dbContext.RifaParticipantes.Any(p => p.CartaRefId == x.Id && p.RifaRefId == id)).ToList();

            return boletosDisponibles;
        }

        [HttpGet("{id:int}/obtener-ganador")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<AddRifaDTO>> GetObtenerGanador(int id)
        {
            var premios = dbContext.Premios.Where(p => !dbContext.Ganadores.Any(g => g.PremioRefId == p.Id)).ToList();
            if (premios.Count == 0)
            {
                return BadRequest("No hay premios por entregar, todos fueron asignados.");
            }
            var participantes = dbContext.RifaParticipantes.Where(p => !dbContext.Ganadores.Any(g => g.ParticipanteRefId == p.ParticipanteRefId)).ToList();
            if (participantes.Count == 0)
            {
                return BadRequest("No hay participantes para entregarles premios.");
            }
            var premio = premios.Find(p => p.Lugar == premios.Max(premio => premio.Lugar));
            var winner = false;
            Random rand = new Random();
            RifaParticipante boletoGanador;
            do
            {
                int number = rand.Next(1, 54);
                boletoGanador = dbContext.RifaParticipantes.FirstOrDefault(db => db.RifaRefId == id && db.CartaRefId == number);
                if (boletoGanador != null)
                {
                    winner = true;
                }
            } while (!winner);

            dbContext.Add(new Ganadores()
            {
                RifaRefId = id,
                PremioRefId = premio.Id,
                ParticipanteRefId = boletoGanador.ParticipanteRefId
            });
            await dbContext.SaveChangesAsync();

            return new AddRifaDTO() { message = $"El boleto ganador es: {boletoGanador.CartaRefId}", data = boletoGanador };
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<GetRifaDTO>> Delete(int id)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                return NotFound($"La rifa con el Id {id} no fue encontrado");
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
                return NotFound($"La Rifa con el id: {id} no fue encontrado");
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
                return NotFound($"La rifa con el Id {id} no fue encontrado");
            }
            dbContext.Rifas.Update(new Rifa()
            {
                Id = id,
                Nombre = objRifa.Nombre
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Rifa>> Search([FromBody] BuscarRifaDTO buscarRifaDTO)
        {
            var aux = dbContext.Rifas.FirstOrDefault(db => db.Nombre == buscarRifaDTO.Nombre);
            if (buscarRifaDTO.Id != 0)
            {
                aux = await dbContext.Rifas.FirstOrDefaultAsync(db => db.Id == buscarRifaDTO.Id);
            }
            else
            {
                aux = dbContext.Rifas.FirstOrDefault(db => db.Nombre == buscarRifaDTO.Nombre);
            }
            return aux;
        }
        [HttpPost("registrar-participante")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AddRifaDTO>> RegistrarParticipante([FromBody] RifaDTOParticipante rifaDTOParticipante)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var roleUser = currentUser.HasClaim(c => c.Type == "EsAdmin");
            var userId = "";
            if (roleUser == false)
            {
                userId = currentUser.FindFirst(c => c.Type == "UserId").Value;
            }
            else
            {
                userId = rifaDTOParticipante.ParticipanteId;
            }
            var veifircarNombreRifa = await dbContext.RifaParticipantes.AnyAsync(x => x.ParticipanteRefId == userId && x.RifaRefId == rifaDTOParticipante.RifaId);
            if (veifircarNombreRifa)
            {
                return BadRequest("El participante ya se encuentra registrado en la rifa.");
            }
            var veifircarCartaRifa = await dbContext.RifaParticipantes.AnyAsync(x => x.CartaRefId == rifaDTOParticipante.CartaId && x.RifaRefId == rifaDTOParticipante.RifaId);
            if (veifircarCartaRifa)
            {
                return BadRequest("El boleto ya se encuentra registrado en la rifa.");
            }
            rifaDTOParticipante.ParticipanteId = userId;

            RifaParticipante rifaParticipanteAux = mapper.Map<RifaParticipante>(rifaDTOParticipante);
            dbContext.Add(rifaParticipanteAux);
            await dbContext.SaveChangesAsync();

            var rifaParticipante = await dbContext.RifaParticipantes.FirstOrDefaultAsync(x => x.ParticipanteRefId == rifaDTOParticipante.ParticipanteId && x.RifaRefId == rifaDTOParticipante.RifaId);

            return new AddRifaDTO() { message = "Participante registrado correctamente", data = rifaParticipante };
        }

        [HttpDelete("{IdRifa:int}/participante/{IdParticipante}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<AddRifaDTO>> RegistrarParticipante(int IdRifa, String IdParticipante)
        {
            var veifircarNombreRifa = await dbContext.RifaParticipantes.AnyAsync(x => x.ParticipanteRefId == IdParticipante && x.RifaRefId == IdRifa);
            if (!veifircarNombreRifa)
            {
                return BadRequest("El participante no se encuentra registrado en la rifa.");
            }

            dbContext.RifaParticipantes.RemoveRange(dbContext.RifaParticipantes.Where(x => x.ParticipanteRefId == IdParticipante && x.RifaRefId == IdRifa));
            await dbContext.SaveChangesAsync();

            return new AddRifaDTO() { message = "Participante eliminado correctamente"};
        }

        [AllowAnonymous]
        [HttpPatch("JsonPatch/{id:int}")]
        public async Task<ActionResult<RifasDtoPatch>> Patch(int id, JsonPatchDocument<Rifa> rifasAux)
        {
            var rifas = await dbContext.Rifas.FirstOrDefaultAsync(a => a.Id == id);
            //  [{"op":"replace", "path": "Nombre", "value": "test"}]
            if (rifas == null)
            {
                return NotFound();
            }
            rifasAux.ApplyTo(rifas);
            await dbContext.SaveChangesAsync();
            return Ok(rifas);
        }
       
        
    }
}
   