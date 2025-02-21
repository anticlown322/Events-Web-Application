using Application.Contracts;
using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.Event;
using Application.DTO.Events;
using AutoMapper;

namespace Application.UseCases.Event;

public class CreateEventUseCase(
    IRepositoryManager repository, 
    IMapper mapper,
    IImageService imageService) : ICreateEventUseCase
{
    public async Task<EventDto> ExecuteAsync(EventForCreationDto eventToCreate, CancellationToken cancellationToken)
    {
        var eventEntity = mapper.Map<Domain.Models.Event>(eventToCreate);
        
        if (eventToCreate.Image != null) 
        {
            await imageService.WriteFileAsync(eventToCreate.Image);
            eventEntity.Image = eventToCreate.Image.FileName;
        }
        
        repository.Event.CreateEvent(eventEntity);
        await repository.SaveAsync();
        
        var eventToReturn = mapper.Map<EventDto>(eventEntity);
        return eventToReturn;
    }
}