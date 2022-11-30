using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class PremioDTO
    {
        //public int Id { get; set; }
        // [ValidarLugar]
        [Range(+1.0, +5.0, ErrorMessage = "Rango error")]
        public int Lugar { get; set; }
        [MinLength(3, ErrorMessage = "Length error")]
        public string Recompensa { get; set; }
    }
}
