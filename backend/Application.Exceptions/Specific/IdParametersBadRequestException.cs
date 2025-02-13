using Application.Exceptions.Base;

namespace Application.Exceptions.Specific;

public sealed class IdParametersBadRequestException : BadRequestException
{
    public IdParametersBadRequestException()
        :base("Parameter ids is null")
    {
    }
}