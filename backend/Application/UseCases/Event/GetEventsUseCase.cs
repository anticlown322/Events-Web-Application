using AutoMapper;
using Application.Contracts.UseCaseContracts.Event;
using Application.DTO.Events;
using Domain.RepositoryContracts;
using Domain.RequestFeatures;

namespace Application.UseCases.Event;

public class GetEventsUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetEventsUseCase
{
    public async Task<(IEnumerable<EventDto> events, MetaData metaData)> ExecuteAsync(
        EventParameters eventParameters, bool trackChanges, CancellationToken cancellationToken)
    {
        var eventsWithMetaData = await repository.Event
            .GetAllEventsAsync(eventParameters, trackChanges, cancellationToken);
        
        var eventsDto = mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);
        return (
            events: eventsDto, 
            metaData: eventsWithMetaData.MetaData);
    }
}