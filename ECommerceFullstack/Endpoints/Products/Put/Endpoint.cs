using FastEndpoints;

namespace Products.Put;

internal sealed class Endpoint(ApplicationDbContext db) : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Put("/products/{id}");
        Roles(nameof(UserRole.Seller));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var product = await Map.ToEntityAsync(req, ct);

        db.Products.Update(product);
        await db.SaveChangesAsync(ct);

        Response.Product = product;
        await SendOkAsync(Response, ct);
    }
}