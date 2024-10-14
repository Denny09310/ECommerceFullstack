using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Products.Get;

internal sealed class Endpoint(ApplicationDbContext db) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response.Products = await db.Products.ToListAsync(ct);
        await SendOkAsync(Response, ct);
    }
}