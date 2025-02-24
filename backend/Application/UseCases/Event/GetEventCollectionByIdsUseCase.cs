using AutoMapper;
using Application.Contracts.UseCaseContracts.Event;
using Application.DTO.Events;
using Application.Validation.Exceptions.Specific;
using Domain.RepositoryContracts;

namespace Application.UseCases.Event;

public class GetEventCollectionByIdsUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetEventCollectionByIdsUseCase
{
    public async Task<IEnumerable<EventDto>> ExecuteAsync(IEnumerable<Guid> ids, bool trackChanges, CancellationToken cancellationToken)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var eventsEntities = await repository.Event.GetEventsByIdsAsync(ids, trackChanges, cancellationToken);
        if (ids.Count() != eventsEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var eventsToReturn = mapper.Map<IEnumerable<EventDto>>(eventsEntities);
        return eventsToReturn;
    }
}