using Domain.Contracts;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Application.Contracts.UseCases.Authentication;
using Shared.DTO.User;

namespace Application.UseCases.UseCases.Authentication;

public class ValidateUserUseCase(
    UserManager<User> userManager, 
    ILoggerManager logger) : IValidateUserUseCase
{
    private User? _user;

    public async Task<bool> ExecuteAsync(UserForAuthenticationDto userForAuth)
    {
        _user = await userManager.FindByNameAsync(userForAuth.UserName);
        var result = _user != null && await userManager.CheckPasswordAsync(_user, userForAuth.Password);

        if (!result)
            logger.LogWarn($"{nameof(ValidateUserUseCase)}: Authentication failed. Wrong username or password.");

        return result;
    }
}