using FluentValidation;

namespace Shared.Validators.Event;

public class EventValidator : AbstractValidator<Domain.Entities.Models.Event>
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 255;
    private const int MaxPlaceLength = 100;
    
    public EventValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.Name)))
            .MaximumLength(MaxNameLength)
            .WithMessage(c => ValidationUtils.TooLongParamMessage(nameof(c.Name), MaxNameLength));
        
        RuleFor(c => c.Description)
            .MaximumLength(MaxDescriptionLength)
            .WithMessage(c => ValidationUtils.TooLongParamMessage(nameof(c.Description), MaxDescriptionLength));
        
        RuleFor(c => c.StartDate)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.StartDate)))
            .Must(date => DateTime.TryParse(date.ToLongDateString(), out _))
            .WithMessage("Invalid date format.");
        
        RuleFor(c => c.Location)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.Location)))
            .MaximumLength(MaxPlaceLength)
            .WithMessage(c => ValidationUtils.TooLongParamMessage(nameof(c.Location), MaxPlaceLength));
        
        RuleFor(c => c.Category)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.Category)));
    }
}