using Application.DTO.User;

namespace Application.Contracts.UseCaseContracts.Authentication;

public interface IRefreshTokenForAuthUseCase
{
    Task<TokenDto> ExecuteAsync(TokenDto tokenDto);
}