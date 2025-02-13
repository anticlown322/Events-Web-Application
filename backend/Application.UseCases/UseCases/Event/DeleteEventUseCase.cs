using Domain.Contracts;
using Application.Contracts;
using Application.Contracts.UseCases.Event;
using Application.Exceptions.Specific;

namespace Application.UseCases.UseCases.Event;

public class DeleteEventUseCase(
    IRepositoryManager repository,
    IImageService imageService) : IDeleteEventUseCase
{
    public async Task ExecuteAsync(Guid eventId, bool trackChanges)
    {
        var eventToDelete = await repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if (eventToDelete is null)
            throw new EventNotFoundByIdException(eventId);

        var filename = eventToDelete.Image;
        if (filename != null)
        {
            imageService.DeleteFile(filename);
        }
        
        repository.Event.DeleteEvent(eventToDelete);
        await repository.SaveAsync();
    }
}