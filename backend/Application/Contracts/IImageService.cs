using Microsoft.AspNetCore.Http;

namespace Application.Contracts;

public interface IImageService
{
    Task<(byte[] fileBytes, string contentType, string filename)> GetImageAsync(string imageFileName);
    Task WriteFileAsync(IFormFile image);
    void DeleteFile(string fileName);
}