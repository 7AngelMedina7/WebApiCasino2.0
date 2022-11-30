using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using WebApiCasino.Entidades;

namespace WebApiCasino.DTOs

{
    [Keyless]
    public class RifaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public DateTime CreationDate { get; set; }
        //premios, participantes y cartas
        //public Premio Premios { get; set; }
        //public Carta Cartas { get; set; }
        //public Participante Participante { get; set; }


        //public virtual ICollection<Premio> Premios { get; set; }
        //public virtual ICollection<Carta> Cartas { get; set; }
        //public virtual ICollection<Participante> Participantes { get; set; }
    }
}
