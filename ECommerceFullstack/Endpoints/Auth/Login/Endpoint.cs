using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using Crypt = BCrypt.Net.BCrypt;

namespace Auth.Login;

internal sealed class Endpoint(ApplicationDbContext db) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == req.Email, ct);
        if (user == null || !Crypt.EnhancedVerify(req.Password, user.Password))
        {
            ThrowError("Authentication Failed.");
        }

        Response.UserId = $"{user.Id}";
        Response.AccessToken = JwtBearer.CreateToken(options =>
        {
            options.SigningKey = Config["Authentication:PrivateKey"]!;
            options.ExpireAt = DateTime.UtcNow.AddDays(1);
            options.User.Claims.AddRange(
            [
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, $"{user.Role}")
            ]);
        });

        await SendOkAsync(Response, ct);
    }
}