namespace Products.Get.Id;

internal sealed class Request
{
    public int Id { get; set; }
}

internal sealed class Response
{
    public Product Product { get; set; }
}