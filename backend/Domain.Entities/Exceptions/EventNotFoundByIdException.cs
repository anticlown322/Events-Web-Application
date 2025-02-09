namespace Domain.Entities.Exceptions;

public sealed class EventNotFoundByIdException : NotFoundException
{
    public EventNotFoundByIdException(Guid eventId)
        :base ($"The event with id: {eventId} does not exist in the database.")
    {
    }
}