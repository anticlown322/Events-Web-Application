namespace Domain.Entities.Exceptions;

public sealed class EventNotFoundException : NotFoundException
{
    public EventNotFoundException(Guid eventId)
        :base ($"The event with id: {eventId} does not exist in the database.")
    {
    }
}