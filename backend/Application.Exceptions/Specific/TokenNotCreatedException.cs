using Application.Exceptions.Base;

namespace Application.Exceptions.Specific;

public sealed class TokenNotCreatedException : UnauthorizedException
{
    public TokenNotCreatedException(string token)
        : base($"Cannot create access or refresh token {token}.")
    {
    }
}