//using AutoMapper;
//using WebApiCasino.DTOs;
//using WebApiCasino.Entidades;

//namespace WebApiCasino.Utilidades
//{
//    public class Automappertest1 : Profile
//    {
//        public Automappertest1()
//        {
//            CreateMap<RifaDTO, Rifa>();
//            CreateMap<Rifa, GetRifaDTO>();
//            CreateMap<Rifa, RifaDTOParticipante>()
//                destino origen
//                .ForMember(d => d.Participantes, o => o.MapFrom(MapRifaDTOParti));
//            CreateMap<ClaseCreacionDTO, Clase>()
//                .ForMember(clase => clase.AlumnoClase, opciones => opciones.MapFrom(MapAlumnoClase));
//            CreateMap<Clase, ClaseDTO>();
//            CreateMap<Clase, ClaseDTOConAlumnos>()
//                .ForMember(claseDTO => claseDTO.Alumnos, opciones => opciones.MapFrom(MapClaseDTOAlumnos));
//            CreateMap<ClasePatchDTO, Clase>().ReverseMap();
//            CreateMap<CursoCreacionDTO, Cursos>();
//            CreateMap<Cursos, CursoDTO>();
//        }

//        private List<ParticipantesDTO> MapRifaDTOParti(Rifa rifa, GetRifaDTO rifaDTO)
//        {
//            var result = new List<Participante>();

//            if (rifa.rifaParticipantes == null) { return result; }

//            foreach (var alumnoClase in rifa.rifaParticipantes)
//            {
//                result.Add(new ParticipantesDTO()
//                {

//                    Id = alumnoClase.RifaId,
//                    Nombre = alumnoClase.RifaNombre
//                });
//            }

//            return result;
//        }

//        private List<GetAlumnoDTO> MapClaseDTOAlumnos(Clase clase, ClaseDTO claseDTO)
//        {
//            var result = new List<GetAlumnoDTO>();

//            if (clase.AlumnoClase == null)
//            {
//                return result;
//            }

//            foreach (var alumnoclase in clase.AlumnoClase)
//            {
//                result.Add(new GetAlumnoDTO()
//                {
//                    Id = alumnoclase.AlumnoId,
//                    Nombre = alumnoclase.Alumno.Nombre
//                });
//            }

//            return result;
//        }

//        private List<AlumnoClase> MapAlumnoClase(ClaseCreacionDTO claseCreacionDTO, Clase clase)
//        {
//            var resultado = new List<AlumnoClase>();

//            if (claseCreacionDTO.AlumnosIds == null) { return resultado; }
//            foreach (var alumnoId in claseCreacionDTO.AlumnosIds)
//            {
//                resultado.Add(new AlumnoClase() { AlumnoId = alumnoId });
//            }
//            return resultado;
//        }
//    }
//}
