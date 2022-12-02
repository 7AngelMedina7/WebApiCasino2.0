using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //Inyeccion de dependencias
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
            //Verifica si el nombre de la rifa ya existe en la base de batos
            var veifircarNombreRifa = await dbContext.Rifas.AnyAsync(x => x.Nombre == obj.Nombre);
            if (veifircarNombreRifa)
            {
                
                return BadRequest($"Ya existe el nombre {obj.Nombre}");
            }
            var rifaAux = mapper.Map<Rifa>(obj);
            dbContext.Add(rifaAux);
            await dbContext.SaveChangesAsync();
            //Busca la rifa para avisar cual es su id
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
                logger.LogError($"La Rifa con el Id: {id} no ha sido encontrado");
                return NotFound($"La Rifa con el Id: {id} no ha sido encontrado");
            }
            return mapper.Map<RifaDTO>(aux);
        }
        //Obtiene los premios de la rifa proporcionada por un "id"
        [HttpGet("{id:int}/premios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GetPremioDTO>> GetPremios(int id)
        {
            var aux = await dbContext.Rifas.FirstOrDefaultAsync(db => db.Id == id);
            if (aux == null)
            {
                logger.LogError($"La Rifa con el Id: {id} no ha sido encontrado");
                return NotFound($"La Rifa con el Id: {id} no ha sido encontrado");
            }
            var verificarPremios = await dbContext.Premios.Where(db => db.RifaRefId == id).ToListAsync();
            if (verificarPremios.Count == 0)
            {
                logger.LogError($"El premio con el Id: {id} no ha sido encontrado");
                return NotFound($"El premio con el Id: {id} no ha sido encontrado");
            }
            GetPremioDTO premioAux = mapper.Map<GetPremioDTO>(verificarPremios);
            return Ok(premioAux);
        }

        //Obtiene los boletos disponibles de una rifa proporcionada dado su "Id"
        [HttpGet("{id:int}/boletos-disponibles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Carta>>> GetBoletosDisponibles(int id)
        {
            List<Carta> boletosDisponibles = dbContext.Cartas.Where(x => !dbContext.RifaParticipantes.Any(p => p.CartaRefId == x.Id && p.RifaRefId == id)).ToList();
            logger.LogInformation($"Obteniendo boletos del id: {id} ");
            return boletosDisponibles;
        }

        //Obtener ganadores dada el "id" de la rifa
        [HttpGet("{id:int}/obtener-ganador")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<AddRifaDTO>> GetObtenerGanador(int id)
        {
            var premios = dbContext.Premios.Where(p => !dbContext.Ganadores.Any(g => g.PremioRefId == p.Id)).ToList();
            if (premios.Count == 0)
            {
                logger.LogWarning($"No hay premios en la rifa {id}");
                return BadRequest("No hay premios por entregar");
            }
            var participantes = dbContext.RifaParticipantes.Where(p => !dbContext.Ganadores.Any(g => g.ParticipanteRefId == p.ParticipanteRefId)).ToList();
            if (participantes.Count == 0)
            {
                logger.LogWarning($"No hay participantes en la rifa {id}");
                return BadRequest("No hay participantes");
            }
            var premio = premios.Find(p => p.Lugar == premios.Max(premio => premio.Lugar));
            //Generar de manera random el boleto
            var ganador = false;
            Random rand = new Random();
            RifaParticipante boletoGanador;
            do
            {  
                int number = rand.Next(1, 54);
                //De manera aleatoria se selecciona un ganador 
                boletoGanador = dbContext.RifaParticipantes.FirstOrDefault(db => db.RifaRefId == id && db.CartaRefId == number);
                if (boletoGanador != null)
                {
                    ganador = true;
                }
            } while (!ganador);

            dbContext.Add(new Ganadores()
            {
                RifaRefId = id,
                PremioRefId = premio.Id,
                ParticipanteRefId = boletoGanador.ParticipanteRefId
            });
            await dbContext.SaveChangesAsync();

            return new AddRifaDTO() { message = $"El boleto ganador es: {boletoGanador.CartaRefId}", data = boletoGanador };
        }
        //Borrar rifa dado su "id"
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<GetRifaDTO>> Delete(int id)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                logger.LogWarning($"La rifa con el Id {id} no fue encontrado");
                return NotFound($"La rifa con el Id {id} no fue encontrado");
            }
            dbContext.Rifas.Remove(new Rifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        //Hacer una modificacion con Patch dado su "id"
        [HttpPatch("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<GetRifaDTO>> Patch(int id, [FromBody] RifaDTO objRifa)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                logger.LogWarning($"La rifa con el Id {id} no fue encontrado");
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

        //Hacer una modificacion con Put dado su "id"
        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<IActionResult> Put(int id, [FromBody] RifaDTO objRifa)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                logger.LogWarning($"La rifa con el Id {id} no fue encontrado");
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
        //Hacer una busqueda dandole como Post el "nombre" de la rifa o el "id" de la rifa
        [HttpPost("search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RifaDTO>> Search([FromBody] BuscarRifaDTO buscarRifaDTO)
        {
            if (buscarRifaDTO.Nombre != null) {

                var aux = dbContext.Rifas.FirstOrDefault(db => db.Nombre == buscarRifaDTO.Nombre);
                if (aux == null)
                {
                    return NotFound("No existe esa rifa");
                }
                RifaDTO rifaauxDto = mapper.Map<RifaDTO>(aux);
                return rifaauxDto;
            }
            else if (buscarRifaDTO.Id != 0)
            {
                var aux = dbContext.Rifas.FirstOrDefault(db => db.Id == buscarRifaDTO.Id);
                if (aux == null)
                {
                    return NotFound("No existe esa rifa");
                }
                RifaDTO rifaauxDto = mapper.Map<RifaDTO>(aux);
                return rifaauxDto;
            }
            return NotFound("No existe esa rifa");
        }
        //Registrar participante en una rifa
        [HttpPost("registrar-participante")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AddRifaDTO>> RegistrarParticipante([FromBody] RifaDTOParticipante rifaDTOParticipante)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var roleUser = currentUser.HasClaim(c => c.Type == "EsAdmin");
            var userId = "";
            //se busca cual es el user y el rol que tiene
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
            //cuando este registrado aparecera un mensaje de "Participante Resgistrado correctamente"

            RifaParticipante rifaParticipanteAux = mapper.Map<RifaParticipante>(rifaDTOParticipante);
            dbContext.Add(rifaParticipanteAux);
            await dbContext.SaveChangesAsync();

            var rifaParticipante = await dbContext.RifaParticipantes.FirstOrDefaultAsync(x => x.ParticipanteRefId == rifaDTOParticipante.ParticipanteId && x.RifaRefId == rifaDTOParticipante.RifaId);

            return new AddRifaDTO() { message = "Participante registrado correctamente", data = rifaParticipante };
        }
        //Borrar participante de una rifa dado el "id" de la rifa y el "id" del participante
        [HttpDelete("{IdRifa:int}/participante/{IdParticipante}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<AddRifaDTO>> RegistrarParticipante(int IdRifa, String IdParticipante)
        {
            var veifircarNombreRifa = await dbContext.RifaParticipantes.AnyAsync(x => x.ParticipanteRefId == IdParticipante && x.RifaRefId == IdRifa);
            if (!veifircarNombreRifa)
            {
                logger.LogWarning($"El participante con el id {IdParticipante} no se encuentra registrado en la rifa.");
                return BadRequest("El participante no se encuentra registrado en la rifa");
            }

            dbContext.RifaParticipantes.RemoveRange(dbContext.RifaParticipantes.Where(x => x.ParticipanteRefId == IdParticipante && x.RifaRefId == IdRifa));
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Participante con el id: {IdParticipante} fue eliminado");
            return new AddRifaDTO() { message = "Participante eliminado"};
        }
        //Uso del JsonPatch
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
   