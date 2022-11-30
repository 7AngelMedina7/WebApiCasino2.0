using System.ComponentModel.DataAnnotations;

using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class ParticipantesDTO
    {
        
        [Required]
        public string Correo { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [Range(1, 54, ErrorMessage = "No")]
        [VerificarNumeroCarta]
        public int Numero_Escogido { get; set; }
    }
}
