using AutoMapper;
using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.Event;
using Application.DTO.Events;
using Application.Validation.Exceptions.Specific;

namespace Application.UseCases.Event;

public class UpdateEventUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IUpdateEventUseCase
{
    public async Task ExecuteAsync(Guid eventId, EventForUpdateDto eventToUpdate, 
        bool trackChanges, CancellationToken cancellationToken)
    {
        var eventEntity = await repository.Event.GetEventByIdAsync(eventId, trackChanges, cancellationToken);
        if (eventEntity is null)
            throw new EventNotFoundByIdException(eventId);

        mapper.Map(eventToUpdate, eventEntity);
        await repository.SaveAsync();
    }
}