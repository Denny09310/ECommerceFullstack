using FastEndpoints;

namespace Orders.Post;

sealed class Mapper : Mapper<Request, Response, IEnumerable<OrderProduct>>
{
    public override Response FromEntity(IEnumerable<OrderProduct> e)
    {
        return new Response
        {
            Products = e.Select(product => new Response.OrderProduct
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
            })
        };
    }
}