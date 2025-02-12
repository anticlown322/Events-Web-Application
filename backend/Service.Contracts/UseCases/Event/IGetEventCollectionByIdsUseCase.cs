using Shared.DTO.Events;

namespace Service.Contracts.UseCases.Event;

public interface IGetEventCollectionByIdsUseCase
{
    Task<IEnumerable<EventDto>> ExecuteAsync(IEnumerable<Guid> ids, bool trackChanges);
}