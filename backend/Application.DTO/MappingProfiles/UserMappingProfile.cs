using AutoMapper;
using Shared.DTO.User;

namespace Application.DTO.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserForRegistrationDto, Domain.Entities.Models.User>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
    }
}