using WebApiCasino.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using WebApiCasino.DTOs.Autenticacion;
using System.Reflection.Metadata;

namespace WebApiCasino
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Carta>().HasData(new Carta[] {
                new Carta(){ Id =1, CartaId = 1, Nombre = "El Gallo ",Persona= ""},
                new Carta(){ Id =2, CartaId = 2, Nombre = "El Diablo ",Persona= ""},
                new Carta(){ Id =3, CartaId = 3, Nombre = "La Dama ",Persona= ""},
                new Carta(){ Id =4, CartaId = 4, Nombre = "El Catrin ",Persona= ""},
                new Carta(){ Id =5, CartaId = 5, Nombre = "El Paraguas ",Persona= ""},
                new Carta(){ Id =6, CartaId = 6, Nombre = "La Sirena ",Persona= ""},
                new Carta(){ Id =7, CartaId = 7, Nombre = "La Escalera ",Persona= ""},
                new Carta(){ Id =8, CartaId = 8, Nombre = "La Botella ",Persona= ""},
                new Carta(){ Id =9, CartaId = 9, Nombre = "El Barril ",Persona= ""},
                new Carta(){ Id =10, CartaId = 10, Nombre = "El Arbol ",Persona= ""},
                new Carta(){ Id =11, CartaId = 11, Nombre = "El Melon ",Persona= ""},
                new Carta(){ Id =12, CartaId = 12, Nombre = "El Valiente ",Persona= ""},
                new Carta(){ Id =13, CartaId = 13, Nombre = "El Gorrito ",Persona= ""},
                new Carta(){ Id =14, CartaId = 14, Nombre = "La Muerte ",Persona= ""},
                new Carta(){ Id =15, CartaId = 15, Nombre = "La Pera ",Persona= ""},
                new Carta(){ Id =16, CartaId = 16, Nombre = "La Bandera ",Persona= ""},
                new Carta(){ Id =17, CartaId = 17, Nombre = "El Bandolon ",Persona= ""},
                new Carta(){ Id =18, CartaId = 18, Nombre = "El Violoncello ",Persona= ""},
                new Carta(){ Id =19, CartaId = 19, Nombre = "La Garza ",Persona= ""},
                new Carta(){ Id =20, CartaId = 20, Nombre = "El Pajaro ",Persona= ""},
                new Carta(){ Id =21, CartaId = 21, Nombre = "La Mano ",Persona= ""},
                new Carta(){ Id =22, CartaId = 22, Nombre = "La Bota ",Persona= ""},
                new Carta(){ Id =23, CartaId = 23, Nombre = "La Luna ",Persona= ""},
                new Carta(){ Id =24, CartaId = 24, Nombre = "El Cotorro ",Persona= ""},
                new Carta(){ Id =25, CartaId = 25, Nombre = "El Borracho ",Persona= ""},
                new Carta(){ Id =26, CartaId = 26, Nombre = "El Negrito ",Persona= ""},
                new Carta(){ Id =27, CartaId = 27, Nombre = "El Corazon ",Persona= ""},
                new Carta(){ Id =28, CartaId = 28, Nombre = "La Sandia ",Persona= ""},
                new Carta(){ Id =29, CartaId = 29, Nombre = "El Tambor ",Persona= ""},
                new Carta(){ Id =30, CartaId = 30, Nombre = "El Camaron ",Persona= ""},
                new Carta(){ Id =31, CartaId = 31, Nombre = "Las Jaras ",Persona= ""},
                new Carta(){ Id =32, CartaId = 32, Nombre = "El Musico ",Persona= ""},
                new Carta(){ Id =33, CartaId = 33, Nombre = "La Araña ",Persona= ""},
                new Carta(){ Id =34, CartaId = 34, Nombre = "El Soldado ",Persona= ""},
                new Carta(){ Id =35, CartaId = 35, Nombre = "La Estrella ",Persona= ""},
                new Carta(){ Id =36, CartaId = 36, Nombre = "El Cazo ",Persona= ""},
                new Carta(){ Id =37, CartaId = 37, Nombre = "El Mundo ",Persona= ""},
                new Carta(){ Id =38, CartaId = 38, Nombre = "El Apache ",Persona= ""},
                new Carta(){ Id =39, CartaId = 39, Nombre = "El Nopal ",Persona= ""},
                new Carta(){ Id =40, CartaId = 40, Nombre = "El Alacran ",Persona= ""},
                new Carta(){ Id =41, CartaId = 41, Nombre = "La Rosa",Persona= ""},
                new Carta(){ Id =42, CartaId = 42, Nombre = "La Calavera ",Persona= ""},
                new Carta(){ Id =43, CartaId = 43, Nombre = "La Campana ",Persona= ""},
                new Carta(){ Id =44, CartaId = 44, Nombre = "El Cantarito ",Persona= ""},
                new Carta(){ Id =45, CartaId = 45, Nombre = "El Venado ",Persona= ""},
                new Carta(){ Id =46, CartaId = 46, Nombre = "El Sol ",Persona= ""},
                new Carta(){ Id =47, CartaId = 47, Nombre = "La Corona ",Persona= ""},
                new Carta(){ Id =48, CartaId = 48, Nombre = "La Chalupa ",Persona= ""},
                new Carta(){ Id =49, CartaId = 49, Nombre = "El Pino ",Persona= ""},
                new Carta(){ Id =50, CartaId = 50, Nombre = "El Pescado ",Persona= ""},
                new Carta(){ Id =51, CartaId = 51, Nombre = "La Palma ",Persona= ""},
                new Carta(){ Id =52, CartaId = 52, Nombre = "La Maceta ",Persona= ""},
                new Carta(){ Id =53, CartaId = 53, Nombre = "El Arpa ",Persona= ""},
                new Carta(){ Id =54, CartaId = 54, Nombre = "La Rana ",Persona= ""}

            });
        }
        //Cuando se cree la bd se creara una tabla con los datos de:
        public DbSet<RifaParticipante> RifaParticipantes { get; set; }
        public DbSet<Rifa> Rifas { get; set; }
        public DbSet<Carta> Cartas { get; set; }
        public DbSet<Premio> Premios { get; set; }
        public DbSet<Ganadores> Ganadores { get; set; }
    }
}