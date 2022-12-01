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

            CreateMap<CrearUsuarioConRifa, Rifa>();
            CreateMap<Rifa, CrearUsuarioConRifa>();

            CreateMap<GetIdPremioDTO, Premio>();
            CreateMap<Premio, GetIdPremioDTO>();

            CreateMap<GetRifaDTO, Rifa>();
            CreateMap<Rifa, GetRifaDTO>();

            CreateMap<RifasDtoPatch, Rifa>();
            CreateMap<Rifa, RifasDtoPatch>();

            CreateMap<Premio, PremioDTO>();
            CreateMap<PremioDTO, Premio>();
        }

        
    }
}
