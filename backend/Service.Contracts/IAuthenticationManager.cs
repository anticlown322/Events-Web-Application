using Domain.Entities.Models;
using Shared.DTO.User;

namespace Service.Contracts;

public interface IAuthenticationManager
{
    Task<TokenDto> CreateToken(User user, bool populateExp);
    public Task<TokenDto> RefreshToken(TokenDto tokenDto);
}