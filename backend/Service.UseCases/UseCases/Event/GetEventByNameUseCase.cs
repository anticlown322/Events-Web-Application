using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Exceptions;
using Service.Contracts.UseCases.Event;
using Shared.DTO.Events;

namespace Service.UseCases.UseCases.Event;

public class GetEventByNameUseCase(
    IRepositoryManager repository, 
    IMapper mapper) : IGetEventByNameUseCase
{
    public async Task<EventDto> ExecuteAsync(string name, bool trackChanges)
    {
        var eventToGet = await repository.Event.GetEventByNameAsync(name, trackChanges);
        if (eventToGet is null)
            throw new EventNotFoundByNameException(name);

        var eventDto = mapper.Map<EventDto>(eventToGet);
        return eventDto;
    }
}