namespace Domain.Entities.Exceptions;

public sealed class EventNotFoundByNameException : NotFoundException
{
    public EventNotFoundByNameException(string name)
        :base ($"The event with name: {name} does not exist in the database.")
    {
    }
}