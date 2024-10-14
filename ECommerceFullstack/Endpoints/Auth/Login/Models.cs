using FastEndpoints;
using FastEndpoints.Security;
using FluentValidation;

namespace Auth.Login;

internal sealed class Request
{
    public string Email { get; set; }
    public string Password { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(p => p.Email)
            .EmailAddress();

        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("The password is required for user authentication.");
    }
}

internal sealed class Response : TokenResponse
{
}