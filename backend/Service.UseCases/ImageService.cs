using Domain.Contracts;
using Domain.Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service.Services;

public class ImageService : IImageService
{
    private readonly string _imageStoragePath;
    private IRepositoryManager _repository;
    
    public ImageService(IConfiguration configuration, IRepositoryManager repository)
    {
        var imageSettings = configuration.GetSection("ImageStorage");
        _imageStoragePath = imageSettings["Path"];
        
        _repository = repository;
    }
    
    public async Task<(byte[] fileBytes, string contentType, string filename)> GetImageAsync(
        Guid eventId, bool trackChanges)
    {
        var imageEvent = await _repository.Event.GetEventByIdAsync(eventId, trackChanges);
        if(imageEvent is null)
            throw new EventNotFoundByIdException(eventId);
        
        var filePath = Path.Combine(_imageStoragePath, imageEvent.Image ?? "");
        
        if (!File.Exists(filePath))
            throw new ImageNotFoundException(filePath);
        
        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var contentType = $"image/{Path.GetExtension(imageEvent.Image).TrimStart('.').ToLowerInvariant()}";
        return (fileBytes, contentType, imageEvent.Image);
    }
    
    public async Task WriteFileAsync(IFormFile image)
    {
        var path = Path.Combine(_imageStoragePath, image.FileName);
        await using var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
    }

    public void DeleteFile(string fileName)
    {
        File.Delete(Path.Combine(_imageStoragePath, fileName));
    }
}