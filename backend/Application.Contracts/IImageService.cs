using Microsoft.AspNetCore.Http;

namespace Application.Contracts;

public interface IImageService
{
    Task<(byte[] fileBytes, string contentType, string filename)> GetImageAsync(Guid eventId, bool trackChanges);
    Task WriteFileAsync(IFormFile image);
    void DeleteFile(string fileName);
}