using System.ComponentModel.DataAnnotations;
using WebApiCasino.Entidades;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class GetRifaDTO
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [ValidarNombres]
        public string Nombre { get; set; }
        //premios y cartas

        public List<PremioDTO> Premios { get; set; }
        public List<CartaDTO> Carta { get; set; }
        public List <ParticipantesDTO> Participantes { get; set; }
    }
}
