using System.ComponentModel.DataAnnotations.Schema;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class GetPremioDTO
    {
        public int Lugar { get; set; }

        public string Recompensa { get; set; }

        [ForeignKey("RifaPremio")]
        public int RifaRefId { get; set; }
    }
}
