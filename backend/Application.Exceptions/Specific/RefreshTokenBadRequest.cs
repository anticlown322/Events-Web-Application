using Application.Exceptions.Base;

namespace Application.Exceptions.Specific;

public sealed class RefreshTokenBadRequest : BadRequestException
{
    public RefreshTokenBadRequest()
        : base("Invalid client request. The tokenDto has some invalid values.")
    {
    }
}