using FastEndpoints;

namespace Orders.Post;

internal sealed class Endpoint(ApplicationDbContext db) : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("/orders");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        // Start a transaction to ensure atomicity
        await using var transaction = await db.Database.BeginTransactionAsync(ct);

        try
        {
            // Create the order entity
            var order = new ECommerceFullstack.Data.Order
            {
                UserId = req.UserId,
            };

            // Add the order to the context and save to generate the Order.Id
            await db.Orders.AddAsync(order, ct);
            await db.SaveChangesAsync(ct); // Save first to generate the Order.Id

            // Prepare the order products now that Order.Id is available
            var orderProducts = req.Products
                .Join(db.Products, p => p.Id, p => p.Id, (p1, p2) => new { p1.Quantity, p2.Id, p2.Price })
                .Select(product => new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = product.Quantity,
                    Price = product.Price
                });

            // Add the order products to the context
            await db.OrderProducts.AddRangeAsync(orderProducts, ct);

            // Save everything (OrderProducts) in one go
            await db.SaveChangesAsync(ct);

            // Commit the transaction
            await transaction.CommitAsync(ct);

            // Map the response after changes are saved
            Response = Map.FromEntity(orderProducts);
            Response.Order = order;

            // Send the response
            await SendOkAsync(Response, ct);
        }
        catch
        {
            // Rollback the transaction in case of any failure
            await transaction.RollbackAsync(ct);
            throw; // Re-throw the exception after rollback
        }
    }
}