using Domain.Contracts;
using Application.Contracts.UseCases.Participant;
using Application.Exceptions.Specific;

namespace Application.UseCases.UseCases.Participant;

public class DeleteParticipantUseCase(
    IRepositoryManager repository) : IDeleteParticipantUseCase
{
    public async Task ExecuteAsync(Guid eventId, Guid participantId, bool trackChanges)
    {
        var participantEvent  = await repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if(participantEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participant = await repository.Participant.GetParticipantByIdAsync(eventId, participantId, trackChanges);
        if (participant is null)
            throw new ParticipantNotFoundException(participantId);
        
        repository.Participant.DeleteParticipant(participant);
        await repository.SaveAsync();
    }
}