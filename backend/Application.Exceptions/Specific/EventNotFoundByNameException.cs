using Application.Exceptions.Base;

namespace Application.Exceptions.Specific;

public sealed class EventNotFoundByNameException : NotFoundException
{
    public EventNotFoundByNameException(string name)
        :base ($"The event with name: {name} does not exist in the database.")
    {
    }
}