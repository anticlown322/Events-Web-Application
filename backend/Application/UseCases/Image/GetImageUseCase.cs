using Application.Contracts;
using Application.Contracts.UseCaseContracts.Image;

namespace Application.UseCases.Image;

public class GetImageUseCase(IImageService imageService): IGetImageUseCase
{
    public async Task<(byte[] fileBytes, string contentType, string filename)> ExecuteAsync(
        Guid eventId, bool trackChanges, CancellationToken cancellationToken)
    {
        var (fileBytes, contentType, filename) =  await imageService
            .GetImageAsync(eventId, trackChanges, cancellationToken);
        
        return (fileBytes, contentType, filename);
    }
}