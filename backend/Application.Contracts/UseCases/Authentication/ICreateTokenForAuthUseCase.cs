using Shared.DTO.User;

namespace Application.Contracts.UseCases.Authentication;

public interface ICreateTokenForAuthUseCase
{
    Task<TokenDto> ExecuteAsync(UserForAuthenticationDto user, bool populateExp);
}