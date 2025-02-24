using Application.Contracts;
using Application.Validation.Exceptions.Specific;
using Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class ImageService : IImageService
{
    private readonly string _imageStoragePath;
    
    public ImageService(IConfiguration configuration, IRepositoryManager repository)
    {
        var imageSettings = configuration.GetSection("ImageStorage");
        _imageStoragePath = imageSettings["Path"];
    }
    
    public async Task<(byte[] fileBytes, string contentType, string filename)> GetImageAsync(string imageFileName)
    {
        var filePath = Path.Combine(_imageStoragePath, imageFileName ?? "");
        
        if (!File.Exists(filePath))
            throw new ImageNotFoundException(filePath);
        
        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var contentType = $"image/{Path.GetExtension(imageFileName).TrimStart('.').ToLowerInvariant()}";
        return (fileBytes, contentType, imageFileName);
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