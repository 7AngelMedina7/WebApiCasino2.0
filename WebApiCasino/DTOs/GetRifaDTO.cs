using WebApiCasino.Entidades;

namespace WebApiCasino.DTOs
{
    public class GetRifaDTO
    {

        public string Nombre { get; set; }
        //premios y cartas

        public List<PremioDTO> Premios { get; set; }
        public List<CartaDTO> Carta { get; set; }
        public List <ParticipantesDTO> Participantes { get; set; }
    }
}
