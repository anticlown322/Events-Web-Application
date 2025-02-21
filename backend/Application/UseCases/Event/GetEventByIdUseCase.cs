using AutoMapper;
using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.Event;
using Application.DTO.Events;
using Application.Validation.Exceptions.Specific;

namespace Application.UseCases.Event;

public class GetEventByIdUseCase(
    IMapper mapper, 
    IRepositoryManager repository) : IGetEventByIdUseCase
{
    public async Task<EventDto> ExecuteAsync(Guid eventId, bool trackChanges, CancellationToken cancellationToken)
    {
        var eventToGet = await repository.Event.GetEventByIdAsync(eventId, trackChanges, cancellationToken);
        if (eventToGet is null)
            throw new EventNotFoundByIdException(eventId);
        
        var eventDto = mapper.Map<EventDto>(eventToGet);
        return eventDto;
    }
}