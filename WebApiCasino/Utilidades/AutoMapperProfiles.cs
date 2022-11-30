using AutoMapper;
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

            CreateMap<Premio, PremioDTO>();
            CreateMap<PremioDTO, Premio>();

            CreateMap<Rifa, GetIdRifaDTO>();
            CreateMap<GetIdRifaDTO, Rifa>();

        }
        
    }
}
