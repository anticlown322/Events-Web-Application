using AutoMapper;
using Application.Contracts.UseCaseContracts.Event;
using Application.DTO.Events;
using Application.Validation.Exceptions.Specific;
using Domain.RepositoryContracts;

namespace Application.UseCases.Event;

public class GetEventByNameUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetEventByNameUseCase
{
    public async Task<EventDto> ExecuteAsync(string name, bool trackChanges, CancellationToken cancellationToken)
    {
        var eventToGet = await repository.Event.GetEventByNameAsync(name, trackChanges, cancellationToken);
        if (eventToGet is null)
            throw new EventNotFoundByNameException(name);
        
        var eventDto = mapper.Map<EventDto>(eventToGet);
        return eventDto;
    }
}