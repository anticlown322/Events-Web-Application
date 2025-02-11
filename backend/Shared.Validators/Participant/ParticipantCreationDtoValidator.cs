using FluentValidation;
using Shared.DTO.Participants;

namespace Shared.Validators.Participant;

public class ParticipantCreationDtoValidator : AbstractValidator<ParticipantForCreationDto>
{
    private const int MaxNameLength = 100;
    private const int MaxSurnameLength = 100;
    
    public ParticipantCreationDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.Name)))
            .MaximumLength(MaxNameLength)
            .WithMessage(c => ValidationUtils.TooLongParamMessage(nameof(c.Name), MaxNameLength));
        
        RuleFor(c => c.Surname)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.Name)))
            .MaximumLength(MaxSurnameLength)
            .WithMessage(c => ValidationUtils.TooLongParamMessage(nameof(c.Name), MaxSurnameLength));
        
        RuleFor(c => c.DateOfBirth)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.DateOfBirth)))
            .Must(date => DateOnly.TryParse(date, out _))
            .WithMessage("Invalid date of birth format.");
        
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.Email)))
            .EmailAddress()
            .WithMessage("Invalid email address.");
        
    }
}