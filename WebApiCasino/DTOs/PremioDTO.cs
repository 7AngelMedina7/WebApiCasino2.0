using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class PremioDTO
    {
        [ValidarLugar]
        [Required]
        public int Lugar { get; set; }

        [ValidarNombres]
        [Required]
        public string Recompensa { get; set; }

        [Required]
        public int RifaId { get; set; }

    }
}
