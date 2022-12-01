using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace WebApiCasino.Validaciones
{
    public class ValidarNombres : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var primeraLetra = value.ToString()[0].ToString();

            if ((value.ToString()).Length <5){
                return new ValidationResult("El nombre debe ser correcto (Mayor a 5 caracteres)");
            }else if (primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe de ser mayuscula");
            }else if((value.ToString()).Length > 100)
            {
                return new ValidationResult("El nombre debe ser mas corto (Menor a 100 caracteres)");
            }
            return ValidationResult.Success;
        }
    }
}
