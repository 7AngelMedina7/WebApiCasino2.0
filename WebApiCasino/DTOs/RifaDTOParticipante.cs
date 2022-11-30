using WebApiCasino.DTOs.Autenticacion;

namespace WebApiCasino.DTOs
{
    public class RifaDTOParticipante:GetRifaDTO
    {
        public List<ParticipantesDTO> Participantes { get; set; }
    }
}
