using Application.Validation.Exceptions.Base;

namespace Application.Validation.Exceptions.Specific;

public sealed class UserAlreadyExistsException : BadRequestException
{
    public UserAlreadyExistsException(string name) 
        : base($"User with {name} username already exists.")
    {
    }
}