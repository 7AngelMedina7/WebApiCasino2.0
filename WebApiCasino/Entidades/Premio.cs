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
        [Range(1,54 ,ErrorMessage ="Solo se puededn añadir de {0} a {54} premios")]
        public int Lugar { get; set; }

        [MinLength(5, ErrorMessage = "Escribe el nombre del premio completo")]
        public string Recompensa { get; set; }

        public virtual ICollection<Rifa> Rifas { get; set; }
       

    }

    //añadir con los DTOS basandote en la tarea
}
