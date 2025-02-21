using Application.DTO.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Contracts.UseCaseContracts.Authentication;

public interface IRegisterUserUseCase
{
    Task<IdentityResult> ExecuteAsync(UserForRegistrationDto userForRegistration);
}