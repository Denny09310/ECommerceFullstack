using FastEndpoints;

namespace Products.Post;

internal sealed class Endpoint(ApplicationDbContext db) : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("/products");
        Roles("Admin", "Seller");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var product = Map.ToEntity(req);

        await db.Products.AddAsync(product, ct);
        await db.SaveChangesAsync(ct);

        Response.Product = product;
        await SendOkAsync(Response, ct);
    }
}