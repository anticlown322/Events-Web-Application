using AutoMapper;
using Domain.Contracts;
using Application.Contracts.UseCases.Event;
using Application.DTO.Events;
using Application.Exceptions.Specific;

namespace Application.UseCases.UseCases.Event;

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