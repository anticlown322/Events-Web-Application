using Application.Validation.Exceptions.Base;

namespace Application.Validation.Exceptions.Specific;

public sealed class IdParametersBadRequestException : BadRequestException
{
    public IdParametersBadRequestException()
        :base("Parameter ids is null")
    {
    }
}