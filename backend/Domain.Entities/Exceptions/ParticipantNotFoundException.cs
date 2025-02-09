namespace Domain.Entities.Exceptions;

public class ParticipantNotFoundException : NotFoundException
{
    public ParticipantNotFoundException(Guid participantId)
        : base($"Participant with id: {participantId} does not exist in the database.")
    {
    }
}