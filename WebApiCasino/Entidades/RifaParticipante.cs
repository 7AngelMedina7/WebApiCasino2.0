using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCasino.Entidades
{
    public class RifaParticipante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RifaParticipanteId { get; set; } 
        
        public string RifaNombre { get; set; }

        [ForeignKey("Rifa")]
        public int RifaRefId { get; set; }
        public Rifa Rifa { get; set; }

        [ForeignKey("Participante")]
        public string ParticipanteRefId { get; set; }
        public IdentityUser Participante { get; set; }

        [ForeignKey("Carta")]
        public int CartaRefId { get; set; }
        public Carta Carta { get; set; }

    }
}
