using AutoMapper;
using Domain.Contracts;
using Application.Contracts.UseCases.Event;
using Application.DTO.Events;
using Application.Exceptions.Specific;

namespace Application.UseCases.UseCases.Event;

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