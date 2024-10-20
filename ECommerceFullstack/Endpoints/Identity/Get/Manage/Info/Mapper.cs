using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace Identity.Get.Manage.Info;

internal sealed class Mapper : ResponseMapper<Response, ApplicationUser>
{
    public override async Task<Response> FromEntityAsync(ApplicationUser entity, CancellationToken ct = default)
    {
        var userManager = Resolve<UserManager<ApplicationUser>>();

        return new Response
        {
            Email = await userManager.GetEmailAsync(entity) ?? throw new NotSupportedException("Users must have an email."),
            IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(entity),
        };
    }
}