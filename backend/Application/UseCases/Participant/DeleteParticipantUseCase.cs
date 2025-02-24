using Application.Contracts.UseCaseContracts.Participant;
using Application.Validation.Exceptions.Specific;
using Domain.RepositoryContracts;

namespace Application.UseCases.Participant;

public class DeleteParticipantUseCase(
    IRepositoryManager repository) : IDeleteParticipantUseCase
{
    public async Task ExecuteAsync(Guid eventId, Guid participantId, 
        bool trackChanges, CancellationToken cancellationToken)
    {
        var participantEvent  = await repository.Event.GetEventByIdAsync(eventId, trackChanges, cancellationToken);
        if(participantEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var participant = await repository.Participant
            .GetParticipantByIdAsync(eventId, participantId, trackChanges, cancellationToken);
        
        if (participant is null)
            throw new ParticipantNotFoundException(participantId);
        
        repository.Participant.DeleteParticipant(participant);
        await repository.SaveAsync();
    }
}