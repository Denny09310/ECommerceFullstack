using FastEndpoints;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;

namespace Identity.Post.Login;

internal sealed class Endpoint(SignInManager<ApplicationUser> signInManager) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/identity/login");
        AllowAnonymous();
        Description(d => d
            .ClearDefaultProduces(200)
            .Produces<AccessTokenResponse>(200));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var useCookieScheme = req.UseCookies || req.UseSessionCookies;
        var isPersistent = req.UseCookies && !req.UseSessionCookies;

        signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;
        var result = await signInManager.PasswordSignInAsync(req.Email, req.Password, isPersistent, lockoutOnFailure: true);

        if (result.RequiresTwoFactor)
        {
            if (!string.IsNullOrEmpty(req.TwoFactorCode))
            {
                result = await signInManager.TwoFactorAuthenticatorSignInAsync(req.TwoFactorCode, isPersistent, rememberClient: isPersistent);
            }
            else if (!string.IsNullOrEmpty(req.TwoFactorRecoveryCode))
            {
                result = await signInManager.TwoFactorRecoveryCodeSignInAsync(req.TwoFactorRecoveryCode);
            }
        }

        if (!result.Succeeded)
        {
            ThrowError(result.ToString());
        }
    }
}