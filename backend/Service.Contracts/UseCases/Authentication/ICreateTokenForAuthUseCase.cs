using Shared.DTO.User;

namespace Service.Contracts.UseCases.Authentication;

public interface ICreateTokenForAuthUseCase
{
    Task<TokenDto> ExecuteAsync(UserForAuthenticationDto user, bool populateExp);
}