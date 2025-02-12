namespace Service.Contracts.UseCases.Event;

public interface IDeleteEventUseCase
{
    Task ExecuteAsync(Guid eventId, bool trackChanges);
}