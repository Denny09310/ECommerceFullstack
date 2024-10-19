namespace ECommerceFullstack.Client.Http.Models;

internal sealed class IdentityLoginResponse
{
    public string AccessToken { get; set; } = default!;
    public long ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = default!;
    public string TokenType { get; set; } = default!;
}