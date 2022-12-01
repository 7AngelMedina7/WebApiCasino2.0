using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCasino.Validaciones;

namespace WebApiCasino.Entidades
{
    public class Rifa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ValidarNombres]
        public string Nombre { get; set; }
        

        //premios y carta
        public virtual ICollection<Premio> Premios { get; set; }

        public virtual ICollection<RifaParticipante> RifaParticipantes { get; set; }
    }
}
