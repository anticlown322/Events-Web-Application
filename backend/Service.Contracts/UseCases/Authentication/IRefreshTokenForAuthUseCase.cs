using Shared.DTO.User;

namespace Service.Contracts.UseCases.Authentication;

public interface IRefreshTokenForAuthUseCase
{
    Task<TokenDto> ExecuteAsync(TokenDto tokenDto);
}