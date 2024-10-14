using FastEndpoints;
using FluentValidation;

namespace Products.Put;

internal sealed class Request
{
    public string? Description { get; set; }
    public int Id { get; set; }
    public string? Image { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Price)
           .GreaterThan(0)
           .WithMessage("Price must be greater than zero.");
    }
}

internal sealed class Response
{
    public Product Product { get; set; }
}