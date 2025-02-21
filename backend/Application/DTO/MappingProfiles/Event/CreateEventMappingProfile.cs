using Application.DTO.Events;
using AutoMapper;

namespace Application.DTO.MappingProfiles.Event;

public class CreateEventMappingProfile : Profile 
{
    public CreateEventMappingProfile()
    {
        CreateMap<EventForCreationDto, Domain.Models.Event>()
            .ForMember(dest => dest.StartDate,
                opts => opts
                    .MapFrom(src => DateTime.Parse(src.StartDate)))
            .ForMember(dest => dest.Image,
                opts => opts
                    .MapFrom(src => src.Image)) ;
    }
}