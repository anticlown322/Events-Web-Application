using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Service.Contracts.UseCases.Event;
using Shared.DTO.Events;

namespace Service.UseCases.UseCases.Event;

public class GetEventByIdUseCase(
    IMapper mapper, 
    IRepositoryManager repository) : IGetEventByIdUseCase
{
    public async Task<EventDto> ExecuteAsync(Guid eventId, bool trackChanges)
    {
        var eventToGet = await repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByIdException(eventId);

        var eventDto = mapper.Map<EventDto>(eventToGet);
        return eventDto;
    }
}