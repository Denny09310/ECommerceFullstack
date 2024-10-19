using FastEndpoints;

namespace Orders.Get;

internal sealed class Mapper : Mapper<Request, Response, List<ECommerceFullstack.Data.Order>>
{
    public override Response FromEntity(List<ECommerceFullstack.Data.Order> entities)
    {
        return new Response
        {
            Orders = entities.Select(o => new Response.Order
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                Total = o.Total,
                Products = o.Products.Select(p => new Response.Product
                {
                    Id = p.Product.Id,
                    Description = p.Product.Description,
                    Image = p.Product.Image,
                    Name = p.Product.Name,
                    Price = p.Product.Price
                })
            })
        };
    }
}