using Application.Contracts;
using Application.Contracts.UseCaseContracts.Authentication;
using Application.DTO.User;
using Application.Validation.Exceptions.Specific;

namespace Application.UseCases.Authentication;

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