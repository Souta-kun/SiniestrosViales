using AutoMapper;
using SiniestrosViales.Application.Dtos;
using SiniestrosViales.Domain.Entities;

namespace SiniestrosViales.Application.Mappings;

public class SiniestroProfile : Profile
{
    public SiniestroProfile()
    {
        CreateMap<Siniestro, SiniestroDto>()
            .ForMember(
                dest => dest.Tipo,
                opt => opt.MapFrom(src => src.Tipo.ToString())
            );
    }
}
