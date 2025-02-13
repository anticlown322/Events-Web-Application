using AutoMapper;
using Domain.Entities.Models;
using Application.DTO.Events;

namespace Application.DTO.MappingProfiles;

public class EventMappingProfile : Profile 
{
    public EventMappingProfile()
    {
        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.StartDate,
                opts => opts.MapFrom(src => src.StartDate.ToString("g")))
            .ForMember(dest => dest.ImageUrl,
                opts => opts
                    .MapFrom(src => src.Image));

        CreateMap<EventForCreationDto, Event>()
            .ForMember(dest => dest.StartDate,
                opts => opts
                    .MapFrom(src => DateTime.Parse(src.StartDate).ToUniversalTime()))
            .ForMember(dest => dest.Image,
                opts => opts
                    .MapFrom(src => src.Image)) ;
        
        CreateMap<EventForUpdateDto, Event>()
            .ForMember(dest => dest.StartDate,
                opts => opts
                    .MapFrom(src => DateTime.Parse(src.StartDate).ToUniversalTime()));
    }
}