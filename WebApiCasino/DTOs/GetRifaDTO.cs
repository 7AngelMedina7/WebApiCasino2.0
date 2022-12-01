using System.ComponentModel.DataAnnotations;
using WebApiCasino.Entidades;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class GetRifaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        

        //public List<PremioDTO> Premios { get; set; }
        //public List<RifaParticipante> RifaParticipantes { get; set; }
        //public List <Usu> Participantes { get; set; }
    }
}
