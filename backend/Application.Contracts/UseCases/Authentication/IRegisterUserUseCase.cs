using Microsoft.AspNetCore.Identity;
using Shared.DTO.User;

namespace Application.Contracts.UseCases.Authentication;

public interface IRegisterUserUseCase
{
    Task<IdentityResult> ExecuteAsync(UserForRegistrationDto userForRegistration);
}