using AutoMapper;
using Domain.Contracts;
using Application.Contracts.UseCases.Event;
using Application.DTO.Events;
using Infrastructure.RequestFeatures;

namespace Application.UseCases.UseCases.Event;

public class GetEventsUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetEventsUseCase
{
    public async Task<(IEnumerable<EventDto> events, MetaData metaData)> ExecuteAsync(
        EventParameters eventParameters, bool trackChanges)
    {
        var eventsWithMetaData = await repository.Event
            .GetAllEventsAsync(eventParameters, trackChanges);
        
        var eventsDto = mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);
        return (
            events: eventsDto, 
            metaData: eventsWithMetaData.MetaData);
    }
}