using Application.Exceptions.Base;

namespace Application.Exceptions.Specific;

public sealed class CollectionByIdsBadRequestException : BadRequestException
{
    public CollectionByIdsBadRequestException()
        :base("Collection count mismatch comparing to ids.")
    {
    }
}
