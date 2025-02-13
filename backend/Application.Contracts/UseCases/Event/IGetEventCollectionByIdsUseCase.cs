using Application.DTO.Events;

namespace Application.Contracts.UseCases.Event;

public interface IGetEventCollectionByIdsUseCase
{
    Task<IEnumerable<EventDto>> ExecuteAsync(IEnumerable<Guid> ids, bool trackChanges);
}