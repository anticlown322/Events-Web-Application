using Application.Validation.Exceptions.Base;

namespace Application.Validation.Exceptions.Specific;

public sealed class CollectionByIdsBadRequestException : BadRequestException
{
    public CollectionByIdsBadRequestException()
        :base("Collection count mismatch comparing to ids.")
    {
    }
}
