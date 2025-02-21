using Application.Validation.Exceptions.Base;

namespace Application.Validation.Exceptions.Specific;

public sealed class EventNotFoundByNameException : NotFoundException
{
    public EventNotFoundByNameException(string name)
        :base ($"The event with name: {name} does not exist in the database.")
    {
    }
}