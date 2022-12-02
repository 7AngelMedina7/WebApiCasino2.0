using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiCasino.DTOs;
using WebApiCasino.Validaciones;

namespace WebApiCasino.Entidades
{
    public class Carta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CartaId { get; set; }
        public string Nombre { get; set; }

        //Validaciones dentro del modelo
        //Verificar numero de la carta
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CartaId != null)
            {
                int valorAux = ((int)CartaId);
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
