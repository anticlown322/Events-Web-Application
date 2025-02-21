using Application.DTO.Events;
using AutoMapper;

namespace Application.DTO.MappingProfiles.Event;

public class UpdateEventMappingProfile : Profile 
{
    public UpdateEventMappingProfile()
    {
        CreateMap<EventForUpdateDto, Domain.Models.Event>()
            .ForMember(dest => dest.StartDate,
                opts => opts
                    .MapFrom(src => DateTime.Parse(src.StartDate).ToUniversalTime()));
    }
}