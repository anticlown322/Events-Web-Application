using Application.Exceptions.Base;

namespace Application.Exceptions.Specific;

public sealed class ParticipantNotFoundException : NotFoundException
{
    public ParticipantNotFoundException(Guid participantId)
        : base($"Participant with id: {participantId} does not exist in the database.")
    {
    }
}