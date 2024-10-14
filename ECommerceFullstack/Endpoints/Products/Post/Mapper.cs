using FastEndpoints;

namespace Products.Post;

sealed class Mapper : Mapper<Request, Response, Product>
{
    public override Product ToEntity(Request req)
    {
        return new Product
        {
            Name = req.Name,
            Description = req.Description,
            Image = req.Image,
            Price = req.Price,
        };
    }
}