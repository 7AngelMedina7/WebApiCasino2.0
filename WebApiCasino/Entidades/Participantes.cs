//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using WebApiCasino.Validaciones;

//namespace WebApiCasino.Entidades
//{
//    public class Participante : Exception
//    {
//        [Required]
//        public int Id { get; set; }
//        [Required]
//        public string Correo { get; set; }

//        [Required]
//        public string Nombre { get; set; }

//        [Required]
//        [Range(1, 54, ErrorMessage = "No")]
//        [VerificarNumeroCarta]
//        public int Numero_Escogido { get; set; }
//        public string Contraseña { get; set; }


//        public class Test : Exception
//        {
//            public Test(string Message) : base(Message)
//            {
//            }
//        }

//    }
//}
