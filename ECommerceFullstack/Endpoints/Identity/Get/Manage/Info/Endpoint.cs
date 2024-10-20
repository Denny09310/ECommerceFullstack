using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace Identity.Get.Manage.Info;

internal sealed class Endpoint(UserManager<ApplicationUser> userManager) : EndpointWithoutRequest<Response, Mapper>
{
    public override void Configure()
    {
        Get("/identity/manage/info");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (await userManager.GetUserAsync(User) is not { } user)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            Response = await Map.FromEntityAsync(user, ct);
            await SendOkAsync(Response, ct);
        }
    }
}