using FluentValidation;
using SS.AuthService.Application.Auth.Commands;

namespace SS.AuthService.Application.Auth.Validators;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]")
                .WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

        RuleFor(x => x.AcceptTos)
            .Equal(true).WithMessage("You must accept the Terms of Service.");

        RuleFor(x => x.AcceptPrivacyPolicy)
            .Equal(true).WithMessage("You must accept the Privacy Policy.");
    }
}
