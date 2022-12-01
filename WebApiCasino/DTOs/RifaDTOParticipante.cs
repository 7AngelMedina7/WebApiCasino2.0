using System.ComponentModel.DataAnnotations;
using WebApiCasino.DTOs.Autenticacion;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class RifaDTOParticipante
    {
        [Required(ErrorMessage = "El Id de Rifa es obligatorio")]
        public int RifaId { get; set; }

        [Required(ErrorMessage = "El Usuario es obligatorio")]
        public string ParticipanteId { get; set; }

        [Required(ErrorMessage = "Lar carta es obligatoria")]
        public int CartaId { get; set; }

    }
}
