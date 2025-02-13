using Shared.DTO.User;

namespace Application.Contracts.UseCases.Authentication;

public interface IValidateUserUseCase
{
    public Task<bool> ExecuteAsync(UserForAuthenticationDto userForAuth);
}