using FastEndpoints;
using System.Security.Claims;

namespace Orders.Get;

internal sealed class Request
{
    [FromClaim(ClaimTypes.NameIdentifier)]
    public int UserId { get; set; }
}

internal sealed class Response
{
    public IEnumerable<Order> Orders { get; set; }

    public sealed class Order
    {
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
        public IEnumerable<Product> Products { get; set; } = [];
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
    }

    public sealed class Product
    {
        public string? Description { get; set; } = default!;
        public int Id { get; set; }
        public string? Image { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}