using Microsoft.AspNetCore.Identity;
using Shared.DTO.User;

namespace Service.Contracts.UseCases.Authentication;

public interface IRegisterUserUseCase
{
    Task<IdentityResult> ExecuteAsync(UserForRegistrationDto userForRegistration);
}