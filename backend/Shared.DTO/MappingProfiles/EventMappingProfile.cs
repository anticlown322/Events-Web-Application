using AutoMapper;
using Domain.Entities.Models;
using Shared.DTO.Events;

namespace Shared.DTO.MappingProfiles;

public class EventMappingProfile : Profile 
{
    public EventMappingProfile()
    {
        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.StartDate,
                opts => opts
                    .MapFrom(src => src.StartDate.ToString("g"))); 

    }
}