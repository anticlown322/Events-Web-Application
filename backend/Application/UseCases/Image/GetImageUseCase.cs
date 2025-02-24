using Application.Contracts;
using Application.Contracts.UseCaseContracts.Image;
using Application.Validation.Exceptions.Specific;
using Domain.RepositoryContracts;

namespace Application.UseCases.Image;

public class GetImageUseCase(
    IImageService imageService,
    IRepositoryManager repository)
    : IGetImageUseCase
{
    public async Task<(byte[] fileBytes, string contentType, string filename)> ExecuteAsync(
        Guid eventId, bool trackChanges, CancellationToken cancellationToken)
    {
        var imageEvent = await repository.Event.GetEventByIdAsync(eventId, trackChanges, cancellationToken);
        if(imageEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var (fileBytes, contentType, filename) =  await imageService.GetImageAsync(imageEvent.Image);
        
        return (fileBytes, contentType, filename);
    }
}