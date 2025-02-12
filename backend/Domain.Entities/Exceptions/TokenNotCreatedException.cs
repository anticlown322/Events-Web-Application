namespace Domain.Entities.Exceptions;

public sealed class TokenNotCreatedException : UnauthorizedException
{
    public TokenNotCreatedException(string token)
        : base($"Cannot create access or refresh token {token}.")
    {
    }
}