using AutoMapper;
using Domain.Contracts;
using Application.Contracts;
using Application.Contracts.UseCases.Event;
using Application.DTO.Events;

namespace Application.UseCases.UseCases.Event;

public class CreateEventUseCase(
    IRepositoryManager repository, 
    IMapper mapper,
    IImageService imageService) : ICreateEventUseCase
{
    public async Task<EventDto> ExecuteAsync(EventForCreationDto eventToCreate)
    {
        var eventEntity = mapper.Map<Domain.Entities.Models.Event>(eventToCreate);
        
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