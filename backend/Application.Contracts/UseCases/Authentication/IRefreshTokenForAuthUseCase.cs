using Shared.DTO.User;

namespace Application.Contracts.UseCases.Authentication;

public interface IRefreshTokenForAuthUseCase
{
    Task<TokenDto> ExecuteAsync(TokenDto tokenDto);
}