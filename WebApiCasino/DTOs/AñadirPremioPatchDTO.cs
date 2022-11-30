using WebApiCasino.Validaciones;

namespace WebApiCasino.DTOs
{
    public class AñadirPremioPatchDTO
    {
        [ValidarMayorACero]
        public int Lugar { get; set; }
        [ValidarNombres]
        public string Recompensa { get; set; }
    }
}
