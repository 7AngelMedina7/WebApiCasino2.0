using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class CartaEscogidaPatchDTO
    {
        [Required]
        [VerificarNumeroCarta]
        public int CartaEscogida { get; set; } 
    }
}
