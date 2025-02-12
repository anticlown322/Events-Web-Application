using Shared.DTO.Events;
using Shared.RequestFeatures;

namespace Service.Contracts.UseCases.Event;

public interface IGetEventsUseCase
{
    Task<(IEnumerable<EventDto> events, MetaData metaData)> ExecuteAsync(
        EventParameters eventParameters, bool trackChanges);
}