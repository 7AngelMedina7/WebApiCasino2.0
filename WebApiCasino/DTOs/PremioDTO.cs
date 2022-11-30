﻿using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class PremioDTO
    {
        [ValidarLugar]
        public int Lugar { get; set; }
        [ValidarNombres]
        public string Recompensa { get; set; }
    }
}
