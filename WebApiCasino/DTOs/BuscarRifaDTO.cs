using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class BuscarRifaDTO
    {
        [ValidarMayorACero]
        public int Id { get; set; }
        [ValidarNombres]

        public string Nombre { get; set; }
    }
}
