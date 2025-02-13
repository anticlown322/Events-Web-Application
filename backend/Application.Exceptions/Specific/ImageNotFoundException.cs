using Application.Exceptions.Base;

namespace Application.Exceptions.Specific;

public sealed class ImageNotFoundException : NotFoundException
{
    public ImageNotFoundException(string path)
        :base ($"Image not found by path: {path}")
    {
    }
}