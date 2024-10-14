namespace Products.Get;

internal sealed class Response
{
    public IEnumerable<Product> Products { get; set; } = [];
}