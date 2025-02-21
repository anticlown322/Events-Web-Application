using Application.DTO.Events;
using Application.RequestFeatures;

namespace Application.Contracts.UseCaseContracts.Event;

public interface IGetEventsUseCase
{
    Task<(IEnumerable<EventDto> events, MetaData metaData)> 
        ExecuteAsync(EventParameters eventParameters, bool trackChanges, CancellationToken cancellationToken);
}