using AutoMapper;
using WebApiCasino.DTOs;
using WebApiCasino.Entidades;

namespace WebApiCasino.Utilidades
{
    public class AutoMapperProfilestest : Profile
    {
        public AutoMapperProfilestest()
        {
            //CreateMap<Rifa, RifaDTO>().ForMember(rifaDTO => rifaDTO.Participante, op => op.MapFrom(MapRifaParticipantes));
            //CreateMap<Participante, ParticipantesDTO>();
            
        }
        //private List<RifaParticipante> MapRifaParticipantes(ParticipanteCreacionDTO participanteCreacionDTO, Participante participante)
        //{
        //    var resultado = new List<RifaParticipante>();

        //    if (participanteCreacionDTO.RifasIds == null) { return resultado; }
        //    foreach (var rifaId in participanteCreacionDTO.RifasIds)
        //    {
        //        resultado.Add(new RifaParticipante() { RifaId = rifaId });
        //    }
        //    return resultado;
        //}
    }
}
