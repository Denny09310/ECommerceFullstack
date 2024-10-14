using FastEndpoints;

using Crypt = BCrypt.Net.BCrypt;

namespace Auth.Register;

internal sealed class Mapper : Mapper<Request, Response, User>
{
    public override User ToEntity(Request req)
    {
        var password = Crypt.EnhancedHashPassword(req.Password);

        return new User
        {
            Email = req.Email,
            Password = password,
        };
    }
}
