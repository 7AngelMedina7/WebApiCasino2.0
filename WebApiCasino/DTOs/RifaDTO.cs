using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using WebApiCasino.Entidades;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class RifaDTO
    {
        [Required(ErrorMessage = "El Id es obligatorio")]
        public int Id { get; set; }
        [ValidarNombres]
        public string Nombre { get; set; }
    }
}
