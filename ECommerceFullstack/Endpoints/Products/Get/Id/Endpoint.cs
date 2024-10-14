using FastEndpoints;

namespace Products.Get.Id;

internal sealed class Endpoint(ApplicationDbContext db) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var product = await db.Products.FindAsync([req.Id], ct);
        if (product == null)
        {
            ThrowError("Product not found.");
        }

        Response.Product = product;
        await SendOkAsync(Response, ct);
    }
}