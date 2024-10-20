using FastEndpoints;

namespace Identity.Post.Register;

internal sealed class Request
{
    public string? Email { get; set; }
    public string Password { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
    }
}