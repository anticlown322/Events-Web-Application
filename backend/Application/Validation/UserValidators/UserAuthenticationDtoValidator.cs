using Application.DTO.User;
using FluentValidation;

namespace Application.Validation.UserValidators;

public class UserAuthenticationDtoValidator : AbstractValidator<UserForAuthenticationDto>
{
    private const int MaxLoginLength = 100;
    private const int MaxPasswordLength = 100;

    public UserAuthenticationDtoValidator()
    {
        RuleFor(l => l.UserName)
            .NotEmpty()
            .WithMessage(l => ValidationUtils.EmptyParamMessage(nameof(l.UserName)))
            .MaximumLength(MaxLoginLength)
            .WithMessage(l => ValidationUtils.TooLongParamMessage(nameof(l.UserName), MaxLoginLength));
        
        RuleFor(l => l.Password)
            .NotEmpty()
            .WithMessage(l => ValidationUtils.EmptyParamMessage(nameof(l.Password)))
            .MaximumLength(MaxPasswordLength)
            .WithMessage(l => ValidationUtils.TooLongParamMessage(nameof(l.Password), MaxPasswordLength));

    }
}