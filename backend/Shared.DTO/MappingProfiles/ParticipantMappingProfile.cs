using AutoMapper;
using Domain.Entities.Models;
using Shared.DTO.Participants;

namespace Shared.DTO.MappingProfiles;

public class ParticipantMappingProfile : Profile
{
    public ParticipantMappingProfile()
    {
        CreateMap<Participant, ParticipantDto>()
            .ForMember(dest => dest.DateOfBirth,
                opts => opts
                    .MapFrom(src => src.DateOfBirth.ToString()))
            .ForMember(dest => dest.RegistrationTime,
                opts 
                    => opts.MapFrom(src => src.RegistrationTime.ToString("g")));
        
        CreateMap<ParticipantForCreationDto, Participant>()
            .ForMember(dest => dest.DateOfBirth,
                opts => opts
                    .MapFrom(src => DateOnly.Parse(src.DateOfBirth)));
    }
}