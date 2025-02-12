using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Service.Contracts.UseCases.Authentication;
using Shared.DTO.User;

namespace Service.UseCases.UseCases.Authentication;

public class RefreshTokenForAuthUseCase(
    IAuthenticationManager authenticationManager) : IRefreshTokenForAuthUseCase
{
    public async Task<TokenDto> ExecuteAsync(TokenDto tokenDto)
    {
        var refreshToken = authenticationManager.RefreshToken(tokenDto);
        
        if(refreshToken.Result.AccessToken is null)
            throw new TokenNotCreatedException(nameof(tokenDto.AccessToken));
        if(refreshToken.Result.RefreshToken is null)
            throw new TokenNotCreatedException(nameof(tokenDto.RefreshToken));
            
        return refreshToken.Result;
    }
}