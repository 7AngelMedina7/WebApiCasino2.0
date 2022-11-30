using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class GetIdRifaDTO
    {
        [Required(ErrorMessage = "El Id es obligatorio")]
        [ValidarMayorACero]
        public int Id { get; set; }
    }
}
