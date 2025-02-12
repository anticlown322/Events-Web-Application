using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Service.Contracts.UseCases.Event;
using Shared.DTO.Events;

namespace Service.UseCases.UseCases.Event;

public class UpdateEventUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IUpdateEventUseCase
{
    public async Task ExecuteAsync(Guid eventId, EventForUpdateDto eventToUpdate, bool trackChanges)
    {
        var eventEntity = await repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (eventEntity is null)
            throw new EventNotFoundByIdException(eventId);

        mapper.Map(eventToUpdate, eventEntity);
        await repository.SaveAsync();
    }
}