using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Application.Contracts;
using Application.Contracts.UseCases.Authentication;
using Application.Exceptions.Specific;
using Shared.DTO.User;

namespace Application.UseCases.UseCases.Authentication;

public class CreateTokenForAuthUseCase(
    IAuthenticationManager authenticationManager,
    UserManager<User> userManager) : ICreateTokenForAuthUseCase
{
    public async Task<TokenDto> ExecuteAsync(UserForAuthenticationDto user, bool populateExp)
    {
        var userEntity = await userManager.FindByNameAsync(user.UserName);
        var tokenDto = await authenticationManager.CreateToken(userEntity, populateExp);
        
        if(tokenDto.AccessToken is null)
            throw new TokenNotCreatedException(nameof(tokenDto.AccessToken));
        if(tokenDto.RefreshToken is null)
            throw new TokenNotCreatedException(nameof(tokenDto.RefreshToken));

        return tokenDto;
    }
}