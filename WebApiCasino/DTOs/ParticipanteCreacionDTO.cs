using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class ParticipanteCreacionDTO
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [ValidarNombres]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<int> RifasIds { get; set; }
    }
}
