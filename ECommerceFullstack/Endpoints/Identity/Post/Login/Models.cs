using FastEndpoints;

namespace Identity.Post.Login;

internal sealed class Request
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string TwoFactorCode { get; set; }

    public string TwoFactorRecoveryCode { get; set; }

    [QueryParam]
    public bool UseCookies { get; set; }

    [QueryParam]
    public bool UseSessionCookies { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
    }
}