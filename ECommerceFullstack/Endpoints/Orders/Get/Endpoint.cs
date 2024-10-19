using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Orders.Get;

internal sealed class Endpoint(ApplicationDbContext db) : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Get("/orders");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var orders = await db.Orders
            .Include(o => o.Products)
            .ThenInclude(op => op.Product)
            .Where(o => o.UserId == req.UserId)
            .ToListAsync(ct);

        Response = Map.FromEntity(orders);
        await SendOkAsync(Response, ct);
    }
}