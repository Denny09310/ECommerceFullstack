using FastEndpoints;

namespace Auth.Register;

internal sealed class Endpoint(ApplicationDbContext db) : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = Map.ToEntity(req);

        await db.Users.AddAsync(user, ct);
        await db.SaveChangesAsync(ct);

        Response.User = user;
        await SendOkAsync(Response, ct);
    }
}