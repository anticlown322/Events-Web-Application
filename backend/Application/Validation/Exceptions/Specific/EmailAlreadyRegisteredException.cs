using Application.Validation.Exceptions.Base;

namespace Application.Validation.Exceptions.Specific;

public class EmailAlreadyRegisteredException : BadRequestException
{
    public EmailAlreadyRegisteredException(Guid eventId, string email)
        :base($"Participant with email {email} already registered to event with id {eventId}")
    {
    }
}