using System.ComponentModel.DataAnnotations;
using System.Drawing;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class CartaEscogidaPatchDTO
    {
        [Required(ErrorMessage = "Escoger una carta es obligatorio")]
        public int CartaEscogida { get; set; }

        //Validar que el numero escogido este entre el 1 y el 54
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CartaEscogida != null)
            {
                int valorAux = ((int)CartaEscogida);
                if (valorAux == 0 || valorAux < 0 || valorAux > 54)
                {
                    yield return new ValidationResult("El numero escogido debe ser entre el 1 al 54");
                }
            }
            else
            {
                yield return ValidationResult.Success;
            }
        }
    }
}
