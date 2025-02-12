using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Service.Contracts.UseCases.Event;
using Shared.DTO.Events;

namespace Service.UseCases.UseCases.Event;

public class GetEventCollectionByIdsUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetEventCollectionByIdsUseCase
{
    public async Task<IEnumerable<EventDto>> ExecuteAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var eventsEntities = await repository.Event.GetEventsByIdsAsync(ids, trackChanges);
        if (ids.Count() != eventsEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var eventsToReturn = mapper.Map<IEnumerable<EventDto>>(eventsEntities);
        return eventsToReturn;
    }
}