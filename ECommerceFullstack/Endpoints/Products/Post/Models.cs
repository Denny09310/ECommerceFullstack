using FastEndpoints;
using FluentValidation;

namespace Products.Post;

internal sealed class Request
{
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Price)
           .GreaterThan(0)
           .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required for new products.");
    }
}

internal sealed class Response
{
    public Product Product { get; set; }
}