using Application.DTO.Events;

namespace Application.Contracts.UseCaseContracts.Event;

public interface IGetEventCollectionByIdsUseCase
{
    Task<IEnumerable<EventDto>> ExecuteAsync(
        IEnumerable<Guid> ids, bool trackChanges, CancellationToken cancellationToken);
}