using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Service.Contracts.UseCases.Authentication;
using Shared.DTO.User;

namespace Service.UseCases.UseCases.Authentication;

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