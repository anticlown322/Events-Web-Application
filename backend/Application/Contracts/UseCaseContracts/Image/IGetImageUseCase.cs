namespace Application.Contracts.UseCaseContracts.Image;

public interface IGetImageUseCase
{
    Task<(byte[] fileBytes, string contentType, string filename)> 
        ExecuteAsync(Guid eventId, bool trackChanges, CancellationToken cancellationToken);
}