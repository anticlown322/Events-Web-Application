using Application.DTO.Events;
using AutoMapper;

namespace Application.DTO.MappingProfiles.Event;

public class GetEventMappingProfile : Profile 
{
    public GetEventMappingProfile()
    {
        CreateMap<Domain.Models.Event, EventDto>()
            .ForMember(dest => dest.StartDate,
                opts => opts.MapFrom(src => src.StartDate.ToString("g")))
            .ForMember(dest => dest.ImageUrl,
                opts => opts
                    .MapFrom(src => src.Image));
    }
}