using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class AddRifaDTO
    {

        [Required]
        public string message { get; set; }

        [Required]
        public Object data { get; set; }

    }
}
