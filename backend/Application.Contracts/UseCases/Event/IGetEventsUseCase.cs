using Application.DTO.Events;
using Infrastructure.RequestFeatures;

namespace Application.Contracts.UseCases.Event;

public interface IGetEventsUseCase
{
    Task<(IEnumerable<EventDto> events, MetaData metaData)> ExecuteAsync(
        EventParameters eventParameters, bool trackChanges);
}