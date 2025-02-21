using Application.DTO.Participants;
using AutoMapper;

namespace Application.DTO.MappingProfiles.Participant;

public class CreateParticipantMappingProfile : Profile
{
    public CreateParticipantMappingProfile()
    {
        CreateMap<ParticipantForCreationDto, Domain.Models.Participant>()
            .ForMember(dest => dest.DateOfBirth,
                opts => opts
                    .MapFrom(src => DateOnly.Parse(src.DateOfBirth)));
    }
}