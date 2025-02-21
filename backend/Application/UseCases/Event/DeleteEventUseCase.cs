using Application.Contracts.RepositoryContracts;
using Application.Contracts;
using Application.Contracts.UseCaseContracts.Event;
using Application.Validation.Exceptions.Specific;

namespace Application.UseCases.Event;

public class DeleteEventUseCase(
    IRepositoryManager repository,
    IImageService imageService) : IDeleteEventUseCase
{
    public async Task ExecuteAsync(Guid eventId, bool trackChanges, CancellationToken cancellationToken)
    {
        var eventToDelete = await repository.Event.GetEventByIdAsync(eventId, trackChanges, cancellationToken);
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