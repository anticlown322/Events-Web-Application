using AutoMapper;
using Domain.Contracts;
using Service.Contracts.UseCases.Event;
using Shared.DTO.Events;
using Shared.RequestFeatures;

namespace Service.UseCases.UseCases.Event;

public class GetEventsUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetEventsUseCase
{
    public async Task<(IEnumerable<EventDto> events, MetaData metaData)> ExecuteAsync(EventParameters eventParameters, bool trackChanges)
    {
        var eventsWithMetaData = await repository.Event.GetAllEventsAsync(eventParameters, trackChanges);

        var eventsDto = mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);

        return (
            events: eventsDto, 
            metaData: eventsWithMetaData.MetaData);
    }
}