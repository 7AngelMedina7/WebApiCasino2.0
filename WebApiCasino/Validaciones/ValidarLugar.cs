using System.ComponentModel.DataAnnotations;

namespace WebApiCasino.Validaciones
{
    public class ValidarLugar : ValidationAttribute
    //Validar que se escriba un numero del 1 al 54
    {
        protected override ValidationResult IsValid(object valor, ValidationContext validationContext)
        {
            if (valor == null || string.IsNullOrEmpty(valor.ToString()))
            {
                return new ValidationResult("Debe escribir un numero");
            }
            int valorAux = int.Parse(valor.ToString());
            if (valorAux <= 0 || valorAux > 5)
            {
                return new ValidationResult("El numero escogido debe ser entre el 1 al 5");
            }

            return ValidationResult.Success;
        }

    }
}
