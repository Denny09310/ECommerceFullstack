using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity.Post.Register;

internal sealed class Endpoint(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore) : Endpoint<Request>
{
    private static readonly EmailAddressAttribute _emailAddressAttribute = new();

    public override void Configure()
    {
        Post("/identity/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"{nameof(Endpoint)} requires a user store with email support.");
        }

        var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
        var email = req.Email;

        if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
        {
            var error = userManager.ErrorDescriber.InvalidEmail(email);
            AddError(error.Description, error.Code);
        }

        ThrowIfAnyErrors();

        var user = new ApplicationUser();
        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await userManager.CreateAsync(user, req.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                AddError(error.Description, error.Code);
            }
        }

        ThrowIfAnyErrors();

        await SendOkAsync(ct);
    }
}