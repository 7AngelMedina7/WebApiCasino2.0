using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class GetIdRifaDTO
    {
        [Required]
        [ValidarMayorACero]
        public int Id { get; set; }
    }
}
