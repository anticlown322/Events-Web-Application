using Application.Validation.Exceptions.Base;

namespace Application.Validation.Exceptions.Specific;

public sealed class ParticipantNotFoundException : NotFoundException
{
    public ParticipantNotFoundException(Guid participantId)
        : base($"Participant with id: {participantId} does not exist in the database.")
    {
    }
}