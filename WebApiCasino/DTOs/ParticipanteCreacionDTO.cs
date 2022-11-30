using System.ComponentModel.DataAnnotations;

namespace WebApiCasino.DTOs
{
    public class ParticipanteCreacionDTO
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Introduce un nombre valido")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<int> RifasIds { get; set; }
    }
}
