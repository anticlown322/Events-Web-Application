using Application.Validation.Exceptions.Base;

namespace Application.Validation.Exceptions.Specific;

public sealed class ImageNotFoundException : NotFoundException
{
    public ImageNotFoundException(string path)
        :base ($"Image not found by path: {path}")
    {
    }
}