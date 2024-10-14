using FastEndpoints;

namespace Products.Delete;

internal sealed class Endpoint(ApplicationDbContext db) : Endpoint<Request>
{
    public override void Configure()
    {
        Delete("/products/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var product = await db.Products.FindAsync([req.Id], ct);
        if (product == null)
        {
            ThrowError("Product not found.");
        }

        product.DeletedAt = DateTime.UtcNow;

        db.Products.Update(product);
        await db.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}