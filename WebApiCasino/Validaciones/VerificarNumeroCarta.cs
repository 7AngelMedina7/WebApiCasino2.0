using System.ComponentModel.DataAnnotations;

namespace WebApiCasino.Validaciones
{
    public class VerificarNumeroCarta:ValidationAttribute
        //Validar que se escriba un numero del 1 al 54
    {
        public VerificarNumeroCarta(): base("El campo {0}, es invalido")
        {
            
        }
        protected override ValidationResult IsValid(object valor, ValidationContext validationContext)
        {
            if(valor != null)
            {
                int valorAux = ((int) valor);
                if (valorAux == 0|| valorAux<0||valorAux>54)
                {
                    return new ValidationResult("El numero escogido debe ser entre el 1 al 54");
                }

            }
            

            return  ValidationResult.Success;
        }

    }
}
