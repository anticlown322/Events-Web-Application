using Shared.DTO.User;

namespace Service.Contracts.UseCases.Authentication;

public interface IValidateUserUseCase
{
    public Task<bool> ExecuteAsync(UserForAuthenticationDto userForAuth);
}