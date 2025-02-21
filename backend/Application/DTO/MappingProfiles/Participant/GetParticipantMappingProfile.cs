using Application.DTO.Participants;
using AutoMapper;

namespace Application.DTO.MappingProfiles.Participant;

public class GetParticipantMappingProfile : Profile
{
    public GetParticipantMappingProfile()
    {
        CreateMap<Domain.Models.Participant, ParticipantDto>()
            .ForMember(dest => dest.DateOfBirth,
                opts => opts
                    .MapFrom(src => src.DateOfBirth.ToString()))
            .ForMember(dest => dest.RegistrationTime,
                opts 
                    => opts.MapFrom(src => src.RegistrationTime.ToString("g")));
    }
}