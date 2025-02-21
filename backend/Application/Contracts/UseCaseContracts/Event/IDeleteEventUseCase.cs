namespace Application.Contracts.UseCaseContracts.Event;

public interface IDeleteEventUseCase
{
    Task ExecuteAsync(Guid eventId, bool trackChanges, CancellationToken cancellationToken);
}