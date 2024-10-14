using FastEndpoints;
using FluentValidation;
using System.Security.Claims;

namespace Orders.Post;

internal sealed class Request
{
    public IEnumerable<Product> Products { get; set; }

    [FromClaim(ClaimTypes.NameIdentifier)]
    public int UserId { get; set; }

    internal sealed class Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleForEach(p => p.Products)
            .SetValidator(new ProductValidator());
    }

    internal sealed class ProductValidator : Validator<Request.Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Product quantity must be greater than 0.");
        }
    }
}

internal sealed class Response
{
    public ECommerceFullstack.Data.Order Order { get; set; }

    public IEnumerable<OrderProduct> Products { get; set; }

    internal sealed class OrderProduct
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}