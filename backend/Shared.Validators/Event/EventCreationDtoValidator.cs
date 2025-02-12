using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.DTO.Events;

namespace Shared.Validators.Event;

public class EventCreationDtoValidator : AbstractValidator<EventForCreationDto>
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 255;
    private const int MaxPlaceLength = 100;
    
    public EventCreationDtoValidator()
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
            .Must(date => DateTime.TryParse(date, out _))
            .WithMessage("Invalid date format.");
        
        RuleFor(c => c.Location)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.Location)))
            .MaximumLength(MaxPlaceLength)
            .WithMessage(c => ValidationUtils.TooLongParamMessage(nameof(c.Location), MaxPlaceLength));
        
        RuleFor(c => c.Category)
            .NotEmpty()
            .WithMessage(c => ValidationUtils.EmptyParamMessage(nameof(c.Category)));
        
        RuleFor(c => c.MaxParticipants)
            .NotEmpty()
            .Must(e => e >= 0)
            .WithMessage("Max participants value cannot be negative.");
        
        RuleFor(c => c.Image)
            .Must(HaveValidImageExtension!)
            .When(c => c.Image != null)
            .WithMessage("Invalid image format.");
    }
    
    private bool HaveValidImageExtension(IFormFile file)
    {
        if (file.Length == 0)
            return false; 
        
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }
}