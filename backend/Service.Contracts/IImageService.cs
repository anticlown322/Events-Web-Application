using Domain.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace Service.Contracts;

public interface IImageService
{
    Task<(byte[] fileBytes, string contentType, string filename)> GetImageAsync(Guid eventId, bool trackChanges);
    Task WriteFileAsync(IFormFile image);
    void DeleteFile(string fileName);
}