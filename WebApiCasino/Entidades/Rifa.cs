using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCasino.DTOs;
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

        public ICollection<Premio> Premios { get; set; }
        public ICollection<RifaParticipante> RifaParticipante { get; set; }
        public ICollection<Ganadores> Ganadores { get; set; }

    }
}
