using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCasino.Validaciones;

namespace WebApiCasino.Entidades
{
    public class Ganadores : Exception
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("RifaGanador")]
        public int RifaRefId { get; set; }
        public Rifa RifaGanador { get; set; }

        [ForeignKey("ParticipanteGanador")]
        public string ParticipanteRefId { get; set; }
        public IdentityUser ParticipanteGanador { get; set; }

        [ForeignKey("PremioGanador")]
        public int PremioRefId { get; set; }
        public Premio PremioGanador { get; set; }

    }
}
