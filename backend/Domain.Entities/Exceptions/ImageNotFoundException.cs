namespace Domain.Entities.Exceptions;

public sealed class ImageNotFoundException : NotFoundException
{
    public ImageNotFoundException(string path)
        :base ($"Image not found by path: {path}")
    {
    }
}