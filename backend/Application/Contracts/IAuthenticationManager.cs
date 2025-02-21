using Application.DTO.User;
using Domain.Models;

namespace Application.Contracts;

public interface IAuthenticationManager
{
    Task<TokenDto> CreateToken(User user, bool populateExp);
    public Task<TokenDto> RefreshToken(TokenDto tokenDto);
}