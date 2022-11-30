using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiCasino.DTOs.Autenticacion
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
