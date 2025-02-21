namespace Application.Contracts.UseCaseContracts.Participant;

public interface IDeleteParticipantUseCase
{
    Task ExecuteAsync(Guid eventId, Guid participantId, bool trackChanges, CancellationToken cancellationToken);
}