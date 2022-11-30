using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace WebApiCasino.Validaciones
{
    public class ValidarNombres:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Debe escribir el nombre");
            }
            else (){

            }
            return base.IsValid(value, validationContext);
        }
    }
}
