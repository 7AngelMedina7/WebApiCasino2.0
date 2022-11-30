using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace WebApiCasino.Validaciones
{
    public class ValidarMayorACero:ValidationAttribute
    {
        //validar que el numero ingresado sea mayor a 0
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Debe escribir un numero");
            }
            int valueAux = int.Parse(value.ToString());
            if(valueAux <= 0)
            {
                return new ValidationResult("El numero ingresado debe ser meyor a 0");
            }
            return ValidationResult.Success;
        }
    }
}
