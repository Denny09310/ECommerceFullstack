using FastEndpoints;

namespace Products.Put;

internal sealed class Mapper : Mapper<Request, Response, Product>
{
    public override async Task<Product> ToEntityAsync(Request req, CancellationToken ct = default)
    {
        var db = Resolve<ApplicationDbContext>();

        var product = await db.Products.FindAsync([req.Id], ct);
        if (product == null)
        {
            ValidationContext<Request>.Instance.ThrowError(r => r.Id, "Product not found.");
        }

        if (!string.IsNullOrEmpty(req.Name))
        {
            product.Name = req.Name;
        }

        if (!string.IsNullOrEmpty(req.Description))
        {
            product.Description = req.Description;
        }

        if (!string.IsNullOrEmpty(req.Image))
        {
            product.Name = req.Image;
        }

        if (product.Price != req.Price)
        {
            product.Price = req.Price;
        }

        return product;
    }
}