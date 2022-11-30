using System.ComponentModel.DataAnnotations;

using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class ParticipantesDTO
    {

        [Required(ErrorMessage = "El Correo es obligatorio")]
        [EmailAddress]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [ValidarNombres]
        public string Nombre { get; set; }

        [Required]
        
        public int Numero_Escogido { get; set; }
    }
}
