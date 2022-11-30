using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApiCasino.DTOs.Autenticacion
{
    public class RegistroDTO
    { 
        [Required(ErrorMessage = "Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        public int IsAdmin { get; set; }
    }
}
