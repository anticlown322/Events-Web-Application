using Application.Contracts;
using Application.Contracts.UseCaseContracts.Authentication;
using Application.DTO.User;
using Application.Validation.Exceptions.Specific;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Authentication;

public class CreateTokenForAuthUseCase(
    IAuthenticationManager authenticationManager,
    UserManager<User> userManager) : ICreateTokenForAuthUseCase
{
    public async Task<TokenDto> ExecuteAsync(UserForAuthenticationDto userDto, bool populateExp)
    {
        var userEntity = await userManager.FindByNameAsync(userDto.UserName);
        if (userEntity == null || !await userManager.CheckPasswordAsync(userEntity, userDto.Password))
        {
            throw new InvalidCredentialsException(userDto.UserName, userDto.Password);
        }
        
        var tokenDto = await authenticationManager.CreateToken(userEntity, populateExp);
        
        if(tokenDto.AccessToken is null)
            throw new TokenNotCreatedException(nameof(tokenDto.AccessToken));
        if(tokenDto.RefreshToken is null)
            throw new TokenNotCreatedException(nameof(tokenDto.RefreshToken));

        return tokenDto;
    }
}