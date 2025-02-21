using Application.DTO.User;

namespace Application.Contracts.UseCaseContracts.Authentication;

public interface ICreateTokenForAuthUseCase
{
    Task<TokenDto> ExecuteAsync(UserForAuthenticationDto userDto, bool populateExp);
}