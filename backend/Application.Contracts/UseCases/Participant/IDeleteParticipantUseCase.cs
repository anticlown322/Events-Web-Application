namespace Application.Contracts.UseCases.Participant;

public interface IDeleteParticipantUseCase
{
    Task ExecuteAsync(Guid eventId, Guid participantId, bool trackChanges);
}