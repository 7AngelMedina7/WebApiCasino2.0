using AutoMapper;
using System.ComponentModel;
using WebApiCasino.DTOs;
using WebApiCasino.Entidades;

namespace WebApiCasino.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Rifa, RifaDTO>();
            CreateMap<RifaDTO, Rifa>();

            CreateMap<Rifa, GetIdRifaDTO>();
            CreateMap<GetIdRifaDTO, Rifa>();

            CreateMap<Premio, AñadirPremioPatchDTO>();
            CreateMap<AñadirPremioPatchDTO, Premio>();

            CreateMap<BuscarRifaDTO, Rifa>();
            CreateMap<Rifa, BuscarRifaDTO>();

            CreateMap<CartaDTO, Carta>();
            CreateMap<Carta, CartaDTO>();

            CreateMap<CartaEscogidaPatchDTO, Carta>();
            CreateMap<Carta, CartaEscogidaPatchDTO>();


            CreateMap<GetRifaDTO, Rifa>();
            CreateMap<Rifa, GetRifaDTO>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest =>
                dest.Nombre,
                opt => opt.MapFrom(src => src.Nombre));

            CreateMap<RifaDTOParticipante, RifaParticipante>()
            .ForMember(dest =>
                dest.RifaRefId,
                opt => opt.MapFrom(src => src.RifaId))
            .ForMember(dest =>
                dest.ParticipanteRefId,
                opt => opt.MapFrom(src => src.ParticipanteId))
            .ForMember(dest =>
                dest.CartaRefId,
                opt => opt.MapFrom(src => src.CartaId));


            CreateMap<GetPremioDTO, Premio>();
            CreateMap<Premio, GetPremioDTO>();

            CreateMap<GetPremioDTO, PremioDTO>().
                ForMember(dest => dest.RifaId, 
                opt => opt.MapFrom(src => src.RifaRefId));

            CreateMap<PremioDTO, GetPremioDTO>();
            CreateMap<Rifa, RifasDtoPatch>();

            CreateMap<Premio, PremioDTO>();
            CreateMap<PremioDTO, Premio>().ForMember(dest =>
                dest.RifaRefId,
                opt => opt.MapFrom(src => src.RifaId));
        }


    }
}
