using Application.DTO.User;
using Domain.Models;

namespace Application.Contracts;

public interface IAuthenticationManager
{
    Task<TokenDto> CreateTokens(User user, bool populateExp);
    Task<string> CreateAccessToken(User user);
}