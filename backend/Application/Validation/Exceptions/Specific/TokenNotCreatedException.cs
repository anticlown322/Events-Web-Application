using Application.Validation.Exceptions.Base;

namespace Application.Validation.Exceptions.Specific;

public sealed class TokenNotCreatedException : UnauthorizedException
{
    public TokenNotCreatedException(string token)
        : base($"Cannot create access or refresh token {token}.")
    {
    }
}