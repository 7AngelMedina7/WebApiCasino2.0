using WebApiCasino.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;
using WebApiCasino.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCasino.Entidades
{
    public class Premio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ValidarLugar]
        public int Lugar { get; set; }

        [ValidarNombres]
        public string Recompensa { get; set; }

        [ForeignKey("RifaPremio")]
        public int RifaRefId { get; set; }
        public Rifa Rifa { get; set; }
    }

    //añadir con los DTOS basandote en la tarea
}
